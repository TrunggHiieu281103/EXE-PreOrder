using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Products.Queries.GetAllProduct;
using Application.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBrandRepositoryAsync : IGenericRepositoryAsync<Brands>
    {
        Task<IReadOnlyList<Brands>> GetBrandPagedReponseWithAssetsAsync(int pageNumber, int pageSize);

        Task<(IReadOnlyList<Brands> Items, int TotalItems)> GetBrandPagedReponseWithAssetsAsync(GetAllBrandsParameter filter);
    }

}