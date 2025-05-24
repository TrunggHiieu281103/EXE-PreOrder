using Application.Features.Brands.Queries.GetBrandById;
using Application.Features.ProductAssets.Queries.GetAssetsByProductId;
using Application.Features.Products.Queries.GetAllProduct;
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

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<BaseResponse<GetProductByIdViewModel>>
    {
        public long Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, BaseResponse<GetProductByIdViewModel>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IMapper _mapper;
            private readonly IOptions<CloudinarySettings> _cloudinarySettings;

            public GetProductByIdQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinarySettings)
            {
                _productRepository = productRepository;
                _mapper = mapper;
                _cloudinarySettings = cloudinarySettings;
            }

            public async Task<BaseResponse<GetProductByIdViewModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetProductByIdAsync(request.Id);
                
                if (product == null)
                    return new BaseResponse<GetProductByIdViewModel>($"Product with Id {request.Id} not found.");

                var productViewModel = _mapper.Map<GetProductByIdViewModel>(product);

                if (productViewModel.ProductAssets != null && productViewModel.ProductAssets.Any())
                {
                    foreach (var asset in productViewModel.ProductAssets)
                    {
                        asset.ImageUrl = $"https://res.cloudinary.com/{_cloudinarySettings.Value.CloudName}/image/upload/{asset.PublicId}.jpg";
                    }
                }
                return new BaseResponse<GetProductByIdViewModel>(productViewModel, $"Get product successfully.");
                
            }
        }
    }
}
