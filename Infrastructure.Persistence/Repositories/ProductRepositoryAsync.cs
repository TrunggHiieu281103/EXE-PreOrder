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

        //public Task<bool> IsUniqueBarcodeAsync(string barcode)
        //{
        //    return _products
        //        .AllAsync(p => p.Barcode != barcode);
        //}
    }
}
