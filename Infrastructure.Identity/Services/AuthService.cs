using Application.DTOs.Auth;
using Application.DTOs.Auth.Login;
using Application.DTOs.Auth.Register;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Repository;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

        public AuthService(
            IUserRepositoryAsync userRepository,
            ITokenService tokenService,
            IPasswordHasher<Users> passwordHasher,
            IMapper mapper,
            IRoleRepositoryAsync roleRepository,
            IUserRoleRepositoryAsync userRoleRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = (await _userRepository.GetUserWithRolesAsync(request.Email, request.Phone));

            if (user == null)
                return new BaseResponse<LoginResponse>("Email or phone number not found. ");

            var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (passwordVerification == PasswordVerificationResult.Failed)
                return new BaseResponse<LoginResponse>("Password incorrect.");

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
            var existingUser = (await _userRepository.GetAllAsync())
                .FirstOrDefault(x => x.Email == request.Email || x.Phone == request.Phone);

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


    }

}
