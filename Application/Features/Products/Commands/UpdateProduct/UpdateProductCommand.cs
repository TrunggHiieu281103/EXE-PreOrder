using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<BaseResponse<long>>
    {
        public long? Id { get; set; } // nullable for safety
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public string? Type { get; set; }
        public string? Size { get; set; }
        public int? StockQuantity { get; set; }
        public string? ProductDetails { get; set; }
        public decimal? Price { get; set; }
        public int? Discount { get; set; }  // phần trăm, ví dụ: 10 = 10%

        public long? OpenedAt { get; set; }
        public bool? IsPreOrder { get; set; }
        public bool? IsActive { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseResponse<long>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public UpdateProductCommandHandler(IProductRepositoryAsync productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<BaseResponse<long>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync((long)command.Id);
                if (product == null)
                {
                    throw new ApiException($"Product Not Found.");
                }
                else
                {
                    product.ProductCode = command.ProductCode ?? product.ProductCode;
                    product.ProductName = command.ProductName ?? product.ProductName;
                    product.Description = command.Description ?? product.Description;
                    product.CategoryId = command.CategoryId ?? product.CategoryId;
                    product.BrandId = command.BrandId ?? product.BrandId;
                    product.Type = command.Type ?? product.Type;
                    product.Size = command.Size ?? product.Size;
                    product.StockQuantity = command.StockQuantity ?? product.StockQuantity;
                    product.ProductDetails = command.ProductDetails ?? product.ProductDetails;
                    product.Price = command.Price ?? product.Price;
                    product.Discount = command.Discount ?? product.Discount;
             
                    product.OpenedAt = command.OpenedAt ?? product.OpenedAt;
                    product.IsPreOrder = command.IsPreOrder ?? product.IsPreOrder;
                    product.IsActive = command.IsActive ?? product.IsActive;

                    // ✅ Tính lại DiscountedPrice nếu có thay đổi về Price hoặc Discount
                    if (command.Price.HasValue || command.Discount.HasValue)
                    {
                        if (product.Discount.HasValue && product.Discount.Value > 0)
                        {
                            product.DiscountedPrice = product.Price - (product.Price * product.Discount.Value / 100);
                        }
                        else
                        {
                            product.DiscountedPrice = product.Price;
                        }
                    }

                    await _productRepository.UpdateAsync(product);
                    return new BaseResponse<long>(product.Id, $"Product update successfully.");

                }
            }
        }
    }
}
