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


        //public Task<bool> IsUniqueBarcodeAsync(string barcode)
        //{
        //    return _products
        //        .AllAsync(p => p.Barcode != barcode);
        //}
    }
}
