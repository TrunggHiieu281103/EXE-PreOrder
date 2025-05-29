using Application.DTOs.Auth;
using Application.DTOs.Auth.Login;
using Application.DTOs.Auth.Register;
using Application.DTOs.Google;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Repository;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Generators;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public AuthService(
            IUserRepositoryAsync userRepository,
            ITokenService tokenService,
            IPasswordHasher<Users> passwordHasher,
            IMapper mapper,
            IRoleRepositoryAsync roleRepository,
            IUserRoleRepositoryAsync userRoleRepository,
            IEmailService emailService,
            IOTPService oTPService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _emailService = emailService;
            _otpService = oTPService;
        }

        public async Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = (await _userRepository.GetUserWithRolesAsync(request.Email, request.Phone));

            if (user == null)
                return new BaseResponse<LoginResponse>("Email or phone number not found. ");

            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (passwordVerification == PasswordVerificationResult.Failed)
                return new BaseResponse<LoginResponse>("Password incorrect.");

            if (user.IsFirstLogin)
            {
                var otp = _emailService.GenerateRandomNumber();
                await _otpService.StoreOTPAsync(user.Email, otp);
                await _emailService.SendOtpMail(user.Email, _emailService._mailSettings.EmailFrom, otp);

                return new BaseResponse<LoginResponse>(null, "OTP sent successful");
            }


            var token = await _tokenService.CreateToken(user);

            var response = new LoginResponse
            {
                AccessToken = token,
                User = _mapper.Map<UserDto>(user)
                
            };

            return new BaseResponse<LoginResponse>(response, "Login successful");
        }

        public async Task<BaseResponse<string>> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetUserWithRolesAsync(request.Email, request.Phone);

            if (existingUser != null)
                 return new BaseResponse<string>("Email or phone number is already registered.");
            

            if (!request.Password.Equals(request.ConfirmPassword))
                return new BaseResponse<string>("Confirm password not match.");
            
            var newUser = new Users
            {
                Email = request.Email,
                Phone = request.Phone,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                IsFirstLogin = true,
                IsEnableTwoFactor = false,
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, request.Password);

                // Add the new user to the DB
                var createdUser = await _userRepository.AddAsync(newUser);
            

            // id 1 is USER
            int roleUserId = 1;
            // --- Assign default role (e.g., "User") ---
            var userRole = await _roleRepository.GetByIdAsync(roleUserId);

            if (userRole == null)
                return new BaseResponse<string>("Default role 'User' not found. Please seed roles first.");

            var userRoles = new UserRoles
            {
                UserId = createdUser.Id,
                RoleId = userRole.Id,
            };

            await _userRoleRepository.AddAsync(userRoles);
           

            return new BaseResponse<string>(newUser.FirstName, "User registered successfully.");
        }

        public async Task<BaseResponse<LoginResponse>> VerifyOTPAsync(string email, string inputOtp)
        {
            var key = email;
            var storedOtp = await _otpService.GetOTPAsync(key);

            if (string.IsNullOrEmpty(storedOtp))
            {
                throw new ApiException(ErrorMessage.Otp_Expried.GetMessage());
            }

            if (storedOtp != inputOtp)
            {
                throw new ApiException("OTP incorrect.");
            }

            // Nếu OTP đúng
            await _otpService.DeleteOTPAsync(key);

            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new ApiException("User not found.");
            }

            if (user.IsFirstLogin)
            {
                user.IsFirstLogin = false;
                await _userRepository.UpdateAsync(user);
            }

            var token = await _tokenService.CreateToken(user);

            var response = new LoginResponse
            {
                AccessToken = token,
                User = _mapper.Map<UserDto>(user)

            };
            return new BaseResponse<LoginResponse>(response, "Login successful");
        }

        public async Task<BaseResponse<string>> ResendOTPAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new ApiException("User not found.");
            }

            var otp = _emailService.GenerateRandomNumber();

            var isStored = await _otpService.StoreOTPAsync(email, otp);
            if (!isStored)
            {
                throw new ApiException(ErrorMessage.Redis_Connection_Failed.GetMessage());
            }

            await _emailService.SendOtpMail(email, _emailService._mailSettings.EmailFrom, otp);

            return new BaseResponse<string>("OTP resend successful.");
        }

        public async Task<BaseResponse<LoginResponse>> GoogleLoginAsync(GoogleLoginRequest request)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
            }
            catch (Exception)
            {
                return new BaseResponse<LoginResponse>("Invalid Google token.");
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(payload.Email);

            if (existingUser == null)
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
                    IsActive = true // đảm bảo được lọc bởi GetUserByEmailAsync
                };

                newUser.Password = _passwordHasher.HashPassword(newUser, "1234567890");

                // 1. Thêm user mới
                await _userRepository.AddAsync(newUser);

                // 2. Lấy lại user vừa thêm để có Id
                var createdUser = await _userRepository.GetUserByEmailAsync(newUser.Email);

                // 3. Gán role mặc định
                var defaultRole = await _roleRepository.GetByIdAsync(1);
                if (defaultRole != null)
                {
                    await _userRoleRepository.AddAsync(new UserRoles
                    {
                        UserId = createdUser.Id,
                        RoleId = defaultRole.Id
                    });
                }

                // ✅ 4. Gọi lại GetUserByEmailAsync để lấy đầy đủ thông tin user + role
                existingUser = await _userRepository.GetUserByEmailAsync(createdUser.Email);
            }

            // 5. Tạo Access Token
            var token = await _tokenService.CreateToken(existingUser);

            // 6. Map sang DTO
            var userDto = _mapper.Map<UserDto>(existingUser);

            // 7. Trả kết quả
            return new BaseResponse<LoginResponse>(new LoginResponse
            {
                AccessToken = token,
                User = userDto
            });
        }

    }

}
