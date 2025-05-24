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
    public interface IProductAssetsRepositoryAsync : IGenericRepositoryAsync<ProductAssets>
    {
        Task<IReadOnlyList<ProductAssets>> GetProductAssetsPagedReponse(long productId);

    }
}
