using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.UpdateUser
{
    public class UpdatePersonalUserInfoCommand : IRequest<BaseResponse<bool>>
    {
        public long UserId { get; private set; }
        public void SetUserId(long id) => UserId = id;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? AvatarKey { get; set; }
        public string? AvatarPublicId { get; set; }
        public string? Phone { get; set; }
        public long? DateOfBirth { get; set; }

        public class UpdatePersonalUserInfoCommandHandler : IRequestHandler<UpdatePersonalUserInfoCommand, BaseResponse<bool>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly IMapper _mapper;

            public UpdatePersonalUserInfoCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<BaseResponse<bool>> Handle(UpdatePersonalUserInfoCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                    return new BaseResponse<bool>("User not found.");

                // Cập nhật từng thuộc tính nếu có giá trị mới
                user.FirstName = string.IsNullOrWhiteSpace(request.FirstName) ? user.FirstName : request.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(request.LastName) ? user.LastName : request.LastName;
                user.Gender = string.IsNullOrWhiteSpace(request.Gender) ? user.Gender : request.Gender;
                user.AvatarKey = string.IsNullOrWhiteSpace(request.AvatarKey) ? user.AvatarKey : request.AvatarKey;
                user.AvatarPublicId = string.IsNullOrWhiteSpace(request.AvatarPublicId) ? user.AvatarPublicId : request.AvatarPublicId;
                user.Phone = string.IsNullOrWhiteSpace(request.Phone) ? user.Phone : request.Phone;
                user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;

                user.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                await _userRepository.UpdateAsync(user);

                return new BaseResponse<bool>(true, "User info updated successfully.");
            }
        }
    }
}
