using Application.Features.ProductAssets.Queries.GetAssetsByProductId;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductAssets.Queries.GetAssetById
{
    public class GetAssetByIdQuery : IRequest<BaseResponse<ProductAssetViewModel>>
    {
        public long Id { get; set; }

        public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, BaseResponse<ProductAssetViewModel>>
        {
            private readonly IProductAssetsRepositoryAsync _productAssetsRepository;
            private readonly IMapper _mapper;
            private readonly IOptions<CloudinarySettings> _cloudinarySettings;

            public GetAssetByIdQueryHandler(IProductAssetsRepositoryAsync productAssetsRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinarySettings)
            {
                _mapper = mapper;
                _productAssetsRepository = productAssetsRepository;
                _cloudinarySettings = cloudinarySettings;
            }

            public async Task<BaseResponse<ProductAssetViewModel>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
            {
                var productAsset = await _productAssetsRepository.GetByIdAsync(request.Id);

                if (productAsset == null)
                    return new BaseResponse<ProductAssetViewModel>($"Asset not found.");
                
                var viewModel = _mapper.Map<ProductAssetViewModel>(productAsset);
                viewModel.ImageUrl = $"https://res.cloudinary.com/{_cloudinarySettings.Value.CloudName}/image/upload/{viewModel.PublicId}.jpg";
                
                return new BaseResponse<ProductAssetViewModel>(viewModel ,$"Get Asset successfully.");
            }
        }
    }
}
