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

namespace Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductsQuery : IRequest<PageResponse<IEnumerable<GetAllProductsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public long? CategoryId { get; set; }   // phải là nullable
        public long? BrandId { get; set; }      // phải là nullable
        public bool? IsPreOrder { get; set; }   // phải là nullable
        public string? Type { get; set; }
        public string? Size { get; set; }
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PageResponse<IEnumerable<GetAllProductsViewModel>>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinarySettings;
        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository, IMapper mapper, IOptions<CloudinarySettings> cloudinarySettings)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cloudinarySettings = cloudinarySettings;
        }

        public async Task<PageResponse<IEnumerable<GetAllProductsViewModel>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllProductsParameter>(request);
            var products = await _productRepository.GetProductPagedReponseWithAssetsAsync(validFilter);

            var productViewModels = _mapper.Map<IEnumerable<GetAllProductsViewModel>>(products);

            // Gán ImageUrl cho từng asset
            foreach (var product in productViewModels)
            {
                if (product.ProductAssets != null && product.ProductAssets.Any())
                {
                    var first = product.ProductAssets.First();
                    first.ImageUrl = $"https://res.cloudinary.com/{_cloudinarySettings.Value.CloudName}/image/upload/{first.PublicId}.jpg";

                    // Chỉ giữ lại 1 asset
                    product.ProductAssets = new List<ProductAssetViewModel> { first };
                }
            }

            return new PageResponse<IEnumerable<GetAllProductsViewModel>>(productViewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
