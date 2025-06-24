using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.SetActiveUser
{
    public class SetActiveUserCommand : IRequest<BaseResponse<bool>>
    {
        public long UserId { get; set; }

        public class SetActiveUserCommandHandler : IRequestHandler<SetActiveUserCommand, BaseResponse<bool>>
        {
            private readonly IUserRepositoryAsync _userRepository;

            public SetActiveUserCommandHandler(IUserRepositoryAsync userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<BaseResponse<bool>> Handle(SetActiveUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user == null)
                    return new BaseResponse<bool>("User not found.");

                if (user.IsActive)
                    return new BaseResponse<bool>("User is already active.");

                user.IsActive = true;
                user.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                await _userRepository.UpdateAsync(user);

                return new BaseResponse<bool>(true, "User account has been reactivated.");
            }
        }
    }
}
