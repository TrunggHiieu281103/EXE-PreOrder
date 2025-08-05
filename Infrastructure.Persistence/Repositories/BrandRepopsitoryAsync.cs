using Application.Features.Brands.Queries.GetAllBrand;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BrandRepositoryAsync : GenericRepositoryAsync<Brands>, IBrandRepositoryAsync
    {
        private readonly DbSet<Brands> _brands;

        public BrandRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _brands = dbContext.Set<Brands>();
        }

        public async Task<IReadOnlyList<Brands>> GetBrandPagedReponseWithAssetsAsync(int pageNumber, int pageSize)
        {
            return await _brands
                //.Where(b => b.IsActive) // Nếu bạn muốn lọc brand đang active
                .OrderBy(b => b.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(IReadOnlyList<Brands> Items, int TotalItems)> GetBrandPagedReponseWithAssetsAsync(GetAllBrandsParameter filter)
        {
            var query = _brands.Where(b => b.IsActive).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(b => b.Name.Contains(filter.Name));

            int totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(b => b.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (items, totalItems);
        }

    }
}