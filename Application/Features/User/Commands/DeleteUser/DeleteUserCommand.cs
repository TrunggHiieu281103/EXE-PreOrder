using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; } 

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
        {
            private readonly IUserRepositoryAsync _userRepository;

            public DeleteUserCommandHandler(IUserRepositoryAsync userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserWithRolesByIdAsync(request.Id);
                if (user == null)
                    return new BaseResponse<bool>("User not found.");

                if (user.UserRoles != null && user.UserRoles.Any(ur => ur.Role != null && ur.Role.RoleName.ToUpper() == "ADMIN"))
                {
                    return new BaseResponse<bool>("Cannot deactivate a user with ADMIN role.");
                }

                user.IsActive = false;
                user.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                await _userRepository.UpdateAsync(user);
                return new BaseResponse<bool>(true, "User has been deactivated.");
            }
        }
    }
}
