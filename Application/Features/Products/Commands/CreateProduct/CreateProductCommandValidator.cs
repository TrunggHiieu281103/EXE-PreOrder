using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepositoryAsync productRepository;

        // đặt các validate cho các input khi tạo Product
        public CreateProductCommandValidator(IProductRepositoryAsync productRepository)
        {
            this.productRepository = productRepository;

            RuleFor(p => p.ProductCode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniqueProductCode).WithMessage("{PropertyName} is existed.");

            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.")
                .MustAsync(IsUniqueProductName).WithMessage("{PropertyName} is existed.");
            
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");
            
            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(FindCategoryId).WithMessage("{PropertyName} not found.");

            RuleFor(p => p.BrandId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .MustAsync(FindBrandId).WithMessage("{PropertyName} not found.");

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");


            RuleFor(p => p.Size)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            
            //RuleFor(p => p.StockQuantity)
            //    .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
            
            //RuleFor(p => p.Price)
            //    .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        }

        private async Task<bool> FindBrandId(long brandId, CancellationToken cancellationToken)
        {
            return await productRepository.FindBrandIdAsync(brandId);
        }

        private async Task<bool> FindCategoryId(long categoryId, CancellationToken cancellationToken)
        {
            return await productRepository.FindCategoryIdAsync(categoryId);
        }

        private async Task<bool> IsUniqueProductCode(string productCode, CancellationToken cancellationToken)
        {
            var product = await productRepository.IsUniqueProductCodeAsync(productCode);
            return product == null;
        }

        private async Task<bool> IsUniqueProductName(string productName, CancellationToken cancellationToken)
        {
            var product = await productRepository.IsUniqueProductNameAsync(productName);
            return product == null;
        }

        //private async Task<bool> IsUniqueBarcode(string barcode, CancellationToken cancellationToken)
        //{
        //    return await productRepository.IsUniqueBarcodeAsync(barcode);
        //}
    }
}
