using Application.Features.Categories.Queries.GetAllCategory;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CategoryRepositoryAsync : GenericRepositoryAsync<Categories>, ICategoryRepositoryAsync
    {
        private readonly DbSet<Categories> _categories;

        public CategoryRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _categories = dbContext.Set<Categories>();
        }

        public async Task<IReadOnlyList<Categories>> GetCategoryPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _categories
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IReadOnlyList<Categories>> GetCategoryPagedResponseAsync(GetAllCategoryParameter filter)
        {
            var query = _categories.Where(c => c.IsActive).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.CategoryName))
            {
                query = query.Where(c => c.CategoryName.Contains(filter.CategoryName));
            }

            return await query
                .OrderBy(c => c.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

    }
}
