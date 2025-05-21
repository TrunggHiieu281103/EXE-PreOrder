using Application.Features.Products.Queries.GetAllProduct;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Products>, IProductRepositoryAsync
    {
        private readonly DbSet<Products> _products;
        private readonly DbSet<Categories> _categories;
        private readonly DbSet<Brands> _brands;

        public ProductRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Products>();
            _brands = dbContext.Set<Brands>();
            _categories = dbContext.Set<Categories>();
        }

        public async Task<IReadOnlyList<Products>> GetProductPagedReponseWithAssetsAsync(GetAllProductsParameter filter)
        {
            var query = _products.Include(p => p.ProductAssets).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.ProductCode))
                query = query.Where(p => p.ProductCode.Contains(filter.ProductCode));
            if (!string.IsNullOrWhiteSpace(filter.ProductName))
                query = query.Where(p => p.ProductName.Contains(filter.ProductName));
            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            if (filter.BrandId.HasValue)
                query = query.Where(p => p.BrandId == filter.BrandId.Value);
            if (filter.IsPreOrder.HasValue)
                query = query.Where(p => p.IsPreOrder == filter.IsPreOrder.Value);
            if (!string.IsNullOrWhiteSpace(filter.Type))
                query = query.Where(p => p.Type.Contains(filter.Type));
            if (!string.IsNullOrWhiteSpace(filter.Size))
                query = query.Where(p => p.Size.Contains(filter.Size));


            return await query
                .OrderBy(p => p.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<Products?> FindDuplicateProductAsync(string productCode, string productName, long brandId, long categoryId)
        {
            return await _products
                .FirstOrDefaultAsync(p =>
                    p.ProductCode == productCode &&
                    p.ProductName == productName &&
                    p.BrandId == brandId &&
                    p.CategoryId == categoryId);
        }

        public async Task<Products?> IsUniqueProductNameAsync(string productName)
        {
            return await _products.FirstOrDefaultAsync(p => p.ProductName == productName);
        }

        public async Task<Products?> IsUniqueProductCodeAsync(string productCode)
        {
            return await _products.FirstOrDefaultAsync(p => p.ProductCode == productCode);
        }

        public async Task<bool> FindBrandIdAsync(long brandId)
        {
            var brand =  await _brands.FindAsync(brandId);
            return brand != null;
        }

        public async Task<bool> FindCategoryIdAsync(long categoryId)
        {
            var category = await _categories.FindAsync(categoryId);
            return category != null;
        }


        //public Task<bool> IsUniqueBarcodeAsync(string barcode)
        //{
        //    return _products
        //        .AllAsync(p => p.Barcode != barcode);
        //}
    }
}
