using Application.Features.Products.Queries.GetAllProduct;
using Application.Features.Products.Queries.GetProductById;
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

namespace Application.Features.ProductAssets.Queries.GetAssetsByProductId
{
    public class GetAssetsByProductIdQuery : IRequest<BaseResponse<IReadOnlyList<ProductAssetViewModel>>>
    {
        public long Id { get; set; }
        //public int PageNumber { get; set; }
        //public int PageSize { get; set; }

        public class GetAssetsByProductIdQueryHandler : IRequestHandler<GetAssetsByProductIdQuery, BaseResponse<IReadOnlyList<ProductAssetViewModel>>>
        {
            private readonly IProductAssetsRepositoryAsync _productAssetsRepository;
            private readonly IMapper _mapper;
            private readonly IOptions<CloudinarySettings> _cloudinarySettings;

            public GetAssetsByProductIdQueryHandler(IProductAssetsRepositoryAsync productAssetsRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinarySettings)
            {
                _mapper = mapper;
                _productAssetsRepository = productAssetsRepository;
                _cloudinarySettings = cloudinarySettings;
            }

            public async Task<BaseResponse<IReadOnlyList<ProductAssetViewModel>>> Handle(GetAssetsByProductIdQuery request, CancellationToken cancellationToken)
            {
                var productAssets = await _productAssetsRepository.GetProductAssetsPagedReponse(request.Id);

                if (productAssets == null || !productAssets.Any())
                {
                    return new BaseResponse<IReadOnlyList<ProductAssetViewModel>> ($"Product with Id {request.Id} not found.");
                }

                var productAssetsViewModel = _mapper.Map<IReadOnlyList<ProductAssetViewModel>>(productAssets);
                foreach (var asset in productAssetsViewModel)
                {
                    asset.ImageUrl = $"https://res.cloudinary.com/{_cloudinarySettings.Value.CloudName}/image/upload/{asset.PublicId}.jpg";
                }

                return new BaseResponse<IReadOnlyList<ProductAssetViewModel>>(productAssetsViewModel, "Get assets successfully.");
            }
        }
    }
}
