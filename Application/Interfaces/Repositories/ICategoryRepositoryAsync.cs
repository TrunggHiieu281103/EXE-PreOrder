using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Categories.Queries.GetAllCategory;
using Application.Repository;

namespace Application.Interfaces.Repositories
{
    public interface ICategoryRepositoryAsync : IGenericRepositoryAsync<Categories>
    {
        Task<IReadOnlyList<Categories>> GetCategoryPagedResponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<Categories>> GetCategoryPagedResponseAsync(GetAllCategoryParameter filter);
    }
}
