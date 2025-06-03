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
    public class ProductAssetsRepositoryAsync : GenericRepositoryAsync<ProductAssets>, IProductAssetsRepositoryAsync
    {
        private readonly DbSet<Products> _products;
        private readonly DbSet<ProductAssets> _productAssets;


        public ProductAssetsRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Products>();
            _productAssets = dbContext.Set<ProductAssets>();

        }
        public async Task<IReadOnlyList<ProductAssets>> GetProductAssetsPagedReponse(long productId)
        {
            return await _productAssets
        .Where(pa => pa.ProductId == productId && pa.IsActive == true)
        .ToListAsync();
        }
    }
}
