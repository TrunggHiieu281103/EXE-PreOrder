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

        public ProductRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Products>();
        }

        public async Task<IReadOnlyList<Products>> GetProductPagedReponseWithAssetsAsync(int pageNumber, int pageSize)
        {
            return await _products
                .Include(p => p.ProductAssets)
                /*.Where(p => p.IsActive == true)*/ // Nếu bạn muốn chỉ lấy sản phẩm còn hoạt động
                .OrderBy(p => p.Id)             // Có thể thay đổi thứ tự sắp xếp tùy yêu cầu
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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


        //public Task<bool> IsUniqueBarcodeAsync(string barcode)
        //{
        //    return _products
        //        .AllAsync(p => p.Barcode != barcode);
        //}
    }
}
