using Application.Interfaces.Repositories;
using Application.Wrappers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands.UpdateUserAvatar
{
    public class UpdateUserAvatarCommand : IRequest<BaseResponse<string>>
    {
        public long UserId {private get; set; }

        public IFormFile File { get; set; }

        public class UpdateUserAvatarCommandHandler : IRequestHandler<UpdateUserAvatarCommand, BaseResponse<string>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly Cloudinary _cloudinary;
            private readonly CloudinarySettings _cloudinarySettings;

            public UpdateUserAvatarCommandHandler(
                IUserRepositoryAsync userRepository,
                IOptions<CloudinarySettings> cloudinarySettings)
            {
                _userRepository = userRepository;
                _cloudinarySettings = cloudinarySettings.Value;
                _cloudinary = new Cloudinary(new Account(
                    _cloudinarySettings.CloudName,
                    _cloudinarySettings.ApiKey,
                    _cloudinarySettings.ApiSecret));
            }

            public async Task<BaseResponse<string>> Handle(UpdateUserAvatarCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                    return new BaseResponse<string>("User not found.");

                // Xóa avatar cũ nếu có
                if (!string.IsNullOrEmpty(user.AvatarPublicId))
                {
                    await _cloudinary.DestroyAsync(new DeletionParams(user.AvatarPublicId));
                }

                // Upload ảnh mới
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(request.File.FileName, request.File.OpenReadStream()),
                    Folder = "avatars",
                    Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                    return new BaseResponse<string>("Upload failed.");

                user.AvatarPublicId = uploadResult.PublicId;
                user.AvatarKey = uploadResult.AssetId;
                user.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                await _userRepository.UpdateAsync(user);

                var imageUrl = $"https://res.cloudinary.com/{_cloudinarySettings.CloudName}/image/upload/{uploadResult.PublicId}.jpg";
                return new BaseResponse<string>(imageUrl, "Avatar updated successfully.");
            }
        }
    }
}
