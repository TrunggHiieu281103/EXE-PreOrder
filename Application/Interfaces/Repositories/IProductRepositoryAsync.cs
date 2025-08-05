using Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Features.Products.Queries.GetAllProduct;

namespace Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Products>
    {
        //Task<bool> IsUniqueBarcodeAsync(string barcode);

        Task<(IReadOnlyList<Products>, int TotalItem)> GetProductPagedReponseWithAssetsAsync(GetAllProductsParameter filter);
        Task<Products?> GetProductByIdAsync(long productId);
        Task<Products?> IsUniqueProductNameAsync(string productName);
        Task<Products?> IsUniqueProductCodeAsync(string productCode);
        Task<bool> FindBrandIdAsync(long brandId);
        Task<bool> FindCategoryIdAsync(long categoryId);
        Task<List<Products>> GetProductsByIdsAsync(IEnumerable<long> productIds);

    }
}
