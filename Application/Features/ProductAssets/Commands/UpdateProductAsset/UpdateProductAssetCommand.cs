using Application.Features.ProductAssets.Queries.GetAssetsByProductId;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ProductAssets.Commands.UpdateProductAsset
{
    public class UpdateProductAssetCommand : IRequest<BaseResponse<ProductAssetViewModel>>
    {
        public long Id { get; set; }
        public IFormFile File { get; set; }

        public class UpdateProductAssetCommandHandler : IRequestHandler<UpdateProductAssetCommand, BaseResponse<ProductAssetViewModel>>
        {
            private readonly IProductAssetsRepositoryAsync _productAssetsRepository;
            private readonly IMapper _mapper;
            private readonly Account _cloudinaryAccount;
            private readonly IOptions<CloudinarySettings> _cloudinarySettings;

            public UpdateProductAssetCommandHandler(
                IProductAssetsRepositoryAsync productAssetsRepository,
                IOptions<CloudinarySettings> cloudinarySettings,
                IMapper mapper)
            {
                _productAssetsRepository = productAssetsRepository;
                _cloudinaryAccount = new Account(
                    cloudinarySettings.Value.CloudName,
                    cloudinarySettings.Value.ApiKey,
                    cloudinarySettings.Value.ApiSecret);
                _cloudinarySettings = cloudinarySettings;
                _mapper = mapper;
            }

            public async Task<BaseResponse<ProductAssetViewModel>> Handle(UpdateProductAssetCommand request, CancellationToken cancellationToken)
            {
                var asset = await _productAssetsRepository.GetByIdAsync(request.Id);

                if (asset == null)
                    return new BaseResponse<ProductAssetViewModel>("Asset not found.");

                // Optional: Xóa ảnh cũ trên Cloudinary
                if (!string.IsNullOrEmpty(asset.PublicId))
                {
                    var cloudinary = new Cloudinary(_cloudinaryAccount);
                    await cloudinary.DestroyAsync(new DeletionParams(asset.PublicId));
                }

                // Upload ảnh mới
                var cloudinaryUpload = new Cloudinary(_cloudinaryAccount);
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(request.File.FileName, request.File.OpenReadStream()),
                    Folder = "products"
                };

                var uploadResult = await cloudinaryUpload.UploadAsync(uploadParams);

                // Cập nhật asset
                asset.PublicId = uploadResult.PublicId;
                asset.MediaKey = uploadResult.AssetId;

                await _productAssetsRepository.UpdateAsync(asset);

                var viewModel = _mapper.Map<ProductAssetViewModel>(asset);
                viewModel.ImageUrl = $"https://res.cloudinary.com/{_cloudinarySettings.Value.CloudName}/image/upload/{asset.PublicId}.jpg";

                return new BaseResponse<ProductAssetViewModel>(viewModel, "Asset updated successfully.");
            }
        }
    }
}
