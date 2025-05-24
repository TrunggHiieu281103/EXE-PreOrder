using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Domain.Entities;

namespace Application.Features.ProductAssets.Commands.UploadProductAsset
{
    public class UploadProductAssetCommand : IRequest<BaseResponse<string>>
    {
        public long ProductId { get; set; }
        public IFormFile File { get; set; }


        public class UploadProductAssetCommandHandler : IRequestHandler<UploadProductAssetCommand, BaseResponse<string>>
        {
            private readonly IProductAssetsRepositoryAsync _productAssetsRepository;
            private readonly Account _cloudinaryAccount;

            public UploadProductAssetCommandHandler(IProductAssetsRepositoryAsync productAssetsRepository, IOptions<CloudinarySettings> cloudinarySettings)
            {
                _productAssetsRepository = productAssetsRepository;
                _cloudinaryAccount = new Account(cloudinarySettings.Value.CloudName, cloudinarySettings.Value.ApiKey, cloudinarySettings.Value.ApiSecret);
            }

            public async Task<BaseResponse<string>> Handle(UploadProductAssetCommand request, CancellationToken cancellationToken)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(request.File.FileName, request.File.OpenReadStream()),
                    Folder = "products"
                };

                var cloudinary = new Cloudinary(_cloudinaryAccount);
                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                var asset = new Domain.Entities.ProductAssets
                {
                    ProductId = request.ProductId,
                    PublicId = uploadResult.PublicId,
                    MediaKey = uploadResult.AssetId
                };

                await _productAssetsRepository.AddAsync(asset);
                return new BaseResponse<string>("Upload success!");
            }
        }

    }
}
