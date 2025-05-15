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

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<BaseResponse<long>>
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public int? StockQuantity { get; set; }
        public string ProductDetails { get; set; }
        public decimal Price { get; set; }
        public long? OpenedAt { get; set; }
        public bool IsPreOrder { get; set; }
    }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse<long>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IMapper _mapper;
            public CreateProductCommandHandler(IProductRepositoryAsync productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<BaseResponse<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var nowMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var product = _mapper.Map<Domain.Entities.Products>(request);

                product.CreatedAt = nowMilliseconds;
                product.UpdatedAt = nowMilliseconds;
                product.IsActive = true;

            await _productRepository.AddAsync(product);
                return new BaseResponse<long>(product.Id, "Product created successfully");
            }
           
        }
    
}
