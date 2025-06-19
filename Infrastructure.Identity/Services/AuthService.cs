using Application.DTOs.Auth;
using Application.DTOs.Auth.ChangePassword;
using Application.DTOs.Auth.ForgetPassword;
using Application.DTOs.Auth.Login;
using Application.DTOs.Auth.Register;
using Application.DTOs.Google;
using Application.DTOs.OTP;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Repository;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Settings;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Generators;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IRoleRepositoryAsync _roleRepository;
        private readonly IUserRoleRepositoryAsync _userRoleRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IOTPService _otpService;
        private readonly IOptions<CloudinarySettings> _cloudinarySettings;

        public AuthService(
            IUserRepositoryAsync userRepository,
            ITokenService tokenService,
            IPasswordHasher<Users> passwordHasher,
            IMapper mapper,
            IRoleRepositoryAsync roleRepository,
            IUserRoleRepositoryAsync userRoleRepository,
            IEmailService emailService,
            IOTPService oTPService,
            IOptions<CloudinarySettings> cloudinarySettings)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _emailService = emailService;
            _otpService = oTPService;
            _cloudinarySettings = cloudinarySettings;
        }

        public async Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                throw new ApiException("Email not found.");

            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (passwordVerification == PasswordVerificationResult.Failed)
                throw new ApiException("Incorrect password.");

            // Nếu user chưa xác thực OTP
            if (user.IsFirstLogin)
            {
                var otp = _emailService.GenerateRandomNumber();
                var isStored = await _otpService.StoreOTPAsync(user.Email, otp);
                if (!isStored)
                    throw new ApiException("Failed to store OTP for verification.");

                await _emailService.SendOtpMail(user.Email, _emailService._mailSettings.EmailFrom, otp);
                return new BaseResponse<LoginResponse>(null, "Your account is not verified. OTP has been sent to your email.");
            }

            // Nếu đã xác thực
            var token = await _tokenService.CreateToken(user);
            var response = new LoginResponse
            {
                AccessToken = token,
                User = _mapper.Map<UserDto>(user)
            };

            return new BaseResponse<LoginResponse>(response, "Login successful.");
        }


        public async Task<BaseResponse<string>> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetUserWithRolesAsync(request.Email, request.Phone);
            if (existingUser != null)
                throw new ApiException("Email or phone number is already registered.");

            if (request.Password != request.ConfirmPassword)
                throw new ApiException("Password and confirmation do not match.");

            var newUser = new Users
            {
                Email = request.Email,
                Phone = request.Phone,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                IsFirstLogin = true,
                IsEnableTwoFactor = false,
                IsActive = true
            };
            newUser.Password = _passwordHasher.HashPassword(newUser, request.Password);

            var createdUser = await _userRepository.AddAsync(newUser);

            var defaultRole = await _roleRepository.GetByIdAsync(1);
            if (defaultRole != null)
            {
                await _userRoleRepository.AddAsync(new UserRoles
                {
                    UserId = createdUser.Id,
                    RoleId = defaultRole.Id
                });
            }

            var otp = _emailService.GenerateRandomNumber();
            await _otpService.StoreOTPAsync(request.Email, otp);
            await _emailService.SendOtpMail(request.Email, _emailService._mailSettings.EmailFrom, otp);

            return new BaseResponse<string>("User created successfully. Please verify your OTP.");
        }

        public async Task<BaseResponse<LoginResponse>> VerifyOTPAsync(string email, string inputOtp)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new ApiException("User not found.");

            if (!user.IsFirstLogin)
                throw new ApiException("User already verified.");

            var storedOtp = await _otpService.GetOTPAsync(email);
            if (storedOtp == null || storedOtp != inputOtp)
                throw new ApiException("Invalid or expired OTP.");

            // Đánh dấu đã xác thực
            user.IsFirstLogin = false;
            await _userRepository.UpdateAsync(user);

            await _otpService.DeleteOTPAsync(email);

            var token = await _tokenService.CreateToken(user);
            return new BaseResponse<LoginResponse>(new LoginResponse
            {
                AccessToken = token,
                User = _mapper.Map<UserDto>(user)
            }, "OTP verified. Login successful.");
        }

        public async Task<BaseResponse<string>> ResendOTPAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || !user.IsFirstLogin)
                throw new ApiException("Cannot resend OTP. User not found or already verified.");

            var otp = _emailService.GenerateRandomNumber();
            var isStored = await _otpService.StoreOTPAsync(email, otp);
            if (!isStored)
                throw new ApiException("Failed to store OTP.");

            await _emailService.SendOtpMail(email, _emailService._mailSettings.EmailFrom, otp);
            return new BaseResponse<string>("OTP resent successfully.");
        }

        public async Task<BaseResponse<LoginResponse>> GoogleLoginAsync(GoogleLoginRequest request)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
            }
            catch
            {
                throw new ApiException("Invalid Google token.");
            }

            var user = await _userRepository.GetUserByEmailAsync(payload.Email);

            if (user == null)
            {
                var newUser = new Users
                {
                    Email = payload.Email,
                    Phone = "NONE",
                    FirstName = payload.GivenName ?? "Google",
                    LastName = payload.FamilyName ?? "User",
                    Gender = "NONE",
                    IsFirstLogin = true,
                    IsEnableTwoFactor = false,
                    IsActive = true
                };

                newUser.Password = _passwordHasher.HashPassword(newUser, "1234567890");

                await _userRepository.AddAsync(newUser);
                var createdUser = await _userRepository.GetUserByEmailAsync(newUser.Email);

                var defaultRole = await _roleRepository.GetByIdAsync(1);
                if (defaultRole != null)
                {
                    await _userRoleRepository.AddAsync(new UserRoles
                    {
                        UserId = createdUser.Id,
                        RoleId = defaultRole.Id
                    });
                }

                user = await _userRepository.GetUserByEmailAsync(createdUser.Email);
            }

            var token = await _tokenService.CreateToken(user);

            return new BaseResponse<LoginResponse>(new LoginResponse
            {
                AccessToken = token,
                User = _mapper.Map<UserDto>(user)
            }, "Google login successful.");
        }

        public async Task<BaseResponse<string>> SendForgotPasswordOTPAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new ApiException("Email not found.");

            if(user.IsFirstLogin)
                throw new ApiException("Your account is not verified.");

            var otp = _emailService.GenerateRandomNumber();
            var stored = await _otpService.StoreForgetPasswordOtpAsync(email, otp);
            if (!stored)
                throw new ApiException("Failed to store OTP.");

            await _emailService.SendResetPassOtpMail(email, _emailService._mailSettings.EmailFrom, otp);
            return new BaseResponse<string>("OTP sent to email.");
        }

        public async Task<BaseResponse<string>> VerifyForgetPasswordOtpAsync(string email, string otp, string newPassword, string confirmPassword)
        {
            var storedOtp = await _otpService.GetForgetPasswordOtpAsync(email);
            if (storedOtp == null)
                throw new ApiException("OTP expired or not found.");

            if (storedOtp != otp)
                throw new ApiException("Invalid OTP.");

            return await ResetPasswordAsync(email, newPassword, confirmPassword);

        }

        private async Task<BaseResponse<string>> ResetPasswordAsync(string email, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
                throw new ApiException("Password and confirmation do not match.");

            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new ApiException("User not found.");

            user.Password = _passwordHasher.HashPassword(user, newPassword);
            await _userRepository.UpdateAsync(user);

            await _otpService.DeleteForgetPasswordOtpAsync(email);

            return new BaseResponse<string>("Password has been reset successfully.");
        }

        public async Task<BaseResponse<UserProfileDto>> GetUserProfileAsync(long userId)
        {
            var user = await _userRepository.GetUserWithRolesByIdAsync(userId);
            if (user == null)
                return new BaseResponse<UserProfileDto>("User not found");

            var avatarPublicId = user.AvatarPublicId ?? "samples/man-portrait";

            var dto = _mapper.Map<UserProfileDto>(user);

            dto.AvatarImageUrl = $"https://res.cloudinary.com/{_cloudinarySettings.Value.CloudName}/image/upload/{dto.AvatarPublicId}.jpg";

            return new BaseResponse<UserProfileDto>(dto);
        }
    }

}
