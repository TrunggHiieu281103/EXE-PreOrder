using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.PreOrder.Queries.GetAllPreOrders;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence.Repositories
{
    public class OrderRepositoryAsync : GenericRepositoryAsync<Orders>, IOrderRepositoryAsync
    {
        private readonly DbSet<Orders> _orders;
        public OrderRepositoryAsync(EXE_PreOrderContext dbContext) : base(dbContext)
        {
            _orders = dbContext.Set<Orders>();
        }

        public async Task<Orders> GetOrderByIdAsync(long id)
        {
            return await _orders
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.Payments)
                .Include(o => o.Shipping)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IReadOnlyList<Orders>> GetOrderPagedResponseAsync(GetAllOrderParameter filter)
        {
            var query = _orders
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.Payments)
                .Include(o => o.Shipping)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                    .Where(o => !o.IsPreorder)
                .AsQueryable();

            // Lọc theo CustomerName nếu có
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                var customerEmail = filter.Email.ToLower();
                query = query.Where(o =>
                    o.User != null &&
                    (
                        o.User.Email.ToLower().Contains(customerEmail) 
                    )
                );
            }

            // Phân trang
            var skip = (filter.PageNumber - 1) * filter.PageSize;

            return await query
                .OrderByDescending(o => o.Id)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Orders>> GetOrderPagedResponseByUserIdAsync(long userId, int pageNumber, int pageSize)
        {
            var query = _orders
                .Where(o => o.UserId == userId)
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.Payments)
                .Include(o => o.Shipping)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .OrderByDescending(o => o.Id).AsQueryable();
             

            var skip = (pageNumber - 1) * pageSize;

            return await query
                .OrderByDescending(o => o.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Orders>> GetPreOrderPagedResponseAsync(GetAllPreOrderParameter filter)
        {
            var query = _orders
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.Payments)
                .Include(o => o.Shipping)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                    .Where(o => o.IsPreorder)
                .AsQueryable();

            // Lọc theo CustomerName nếu có
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                var customerEmail = filter.Email.ToLower();
                query = query.Where(o =>
                    o.User != null &&
                    (
                        o.User.Email.ToLower().Contains(customerEmail)
                    )
                );
            }

            // Phân trang
            var skip = (filter.PageNumber - 1) * filter.PageSize;

            return await query
                .OrderByDescending(o => o.Id)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToListAsync();
        }
    }
}
