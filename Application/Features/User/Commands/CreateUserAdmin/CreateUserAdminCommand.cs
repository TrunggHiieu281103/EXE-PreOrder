using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.CreateUserAdmin
{
    public class CreateUserAdminCommand : IRequest<BaseResponse<long>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public class CreateUserCommandHandler : IRequestHandler<CreateUserAdminCommand, BaseResponse<long>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly IRoleRepositoryAsync _roleRepository;
            private readonly IUserRoleRepositoryAsync _userRoleRepository;
            private readonly IPasswordHasher<Users> _passwordHasher;

            public CreateUserCommandHandler(
                IUserRepositoryAsync userRepository,
                IRoleRepositoryAsync roleRepository,
                IUserRoleRepositoryAsync userRoleRepository,
                IPasswordHasher<Users> passwordHasher)
            {
                _userRepository = userRepository;
                _roleRepository = roleRepository;
                _userRoleRepository = userRoleRepository;
                _passwordHasher = passwordHasher;
            }

            public async Task<BaseResponse<long>> Handle(CreateUserAdminCommand request, CancellationToken cancellationToken)
            {
                // Kiểm tra Email đã tồn tại chưa
                var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
                if (existingUser != null)
                    return new BaseResponse<long>("Email already in use.");

                // Tạo user mới
                var newUser = new Users
                {
                    Email = request.Email,
                    Phone = request.Phone,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender,
                    IsFirstLogin = true,
                    IsEnableTwoFactor = false,
                    IsActive = true,
                    CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };

                newUser.Password = _passwordHasher.HashPassword(newUser, request.Password);

                // Lưu vào DB
                var createdUser = await _userRepository.AddAsync(newUser);

                // Gán role mặc định là ADMIN (id = 2)
                const long adminRoleId = 2;
                var adminRole = await _roleRepository.GetByIdAsync(adminRoleId);
                if (adminRole != null)
                {
                    await _userRoleRepository.AddAsync(new UserRoles
                    {
                        UserId = createdUser.Id,
                        RoleId = adminRole.Id
                    });
                }

                return new BaseResponse<long>(createdUser.Id, "User created successfully with ADMIN role.");
            }
        }
    }
}
