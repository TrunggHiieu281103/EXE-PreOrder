using Application.DTOs.DashBoard;
using Application.Enums;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.PreOrder.Queries.GetAllPreOrders;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<(IReadOnlyList<Orders>, int TotalItems)> GetOrderPagedResponseAsync(GetAllOrderParameter filter)
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

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(o => o.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (items, totalItems);
        }

        public async Task<(IReadOnlyList<Orders>, int TotalItems)> GetOrderPagedResponseByUserIdAsync(long userId, int pageNumber, int pageSize)
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
             
            var totalItems = await query.CountAsync();

            var itmes = await query
                .OrderByDescending(o => o.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return ( itmes, totalItems );
        }

        public async Task<(IReadOnlyList<Orders>, int TotalItems)> GetPreOrderPagedResponseAsync(GetAllPreOrderParameter filter)
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
            if (!string.IsNullOrWhiteSpace(filter.UserEmail))
            {
                var customerEmail = filter.UserEmail.ToLower();
                query = query.Where(o =>
                    o.User != null &&
                    (
                        o.User.Email.ToLower().Contains(customerEmail)
                    )
                );
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(o => o.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (items, totalItems);
        }

        public async Task<(IReadOnlyList<Orders>, int TotalItems)> GetUserOrdersAsync(long userId, int pageNumber, int pageSize, bool? isPreorder)
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

            if (isPreorder.HasValue)
            {
                query = query.Where(o => o.IsPreorder == isPreorder.Value);
            }
            var skip = (pageNumber - 1) * pageSize;
            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(o => o.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }
        public async Task<int> CountAllOrdersAsync()
        {
            return await _orders.CountAsync();
        }

        public async Task<int> CountOrdersByPaymentStatusAsync(string status)
        {
            return await _orders
                .Where(o => o.Payments.Any(p => p.PaymentStatus == status))
                .CountAsync();
        }

        public async Task<int> CountOrdersByTypeAsync(bool isPreorder)
        {
            return await  _orders
                .Where(o => o.IsPreorder == isPreorder)
                .CountAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _orders
                .Where(o => o.Status == OrderStatusEnum.COMPLETED.ToString()) 
                .SumAsync(o => (o.TotalPrice ?? 0) + (o.ShippingFee ?? 0)); 
        }

        public async Task<List<Orders>> GetOrdersWithPaymentsAsync(Expression<Func<Orders, bool>> predicate)
        {
            return await _orders
                .Where(predicate)
                .Include(o => o.Payments)
                .ToListAsync();
        }

        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(int year)
        {
            var orders = await _orders.ToListAsync();

            var result = orders
                .Where(o =>
                {
                    var createdDate = DateTimeOffset.FromUnixTimeMilliseconds(o.CreatedAt).DateTime;
                    return createdDate.Year == year && o.Status == OrderStatusEnum.COMPLETED.ToString();
                })
                .GroupBy(o => DateTimeOffset.FromUnixTimeMilliseconds(o.CreatedAt).DateTime.Month)
                .Select(g => new MonthlyRevenueDto
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(o => (o.ShippingFee ?? 0) + (o.TotalPrice ?? 0))
                })
                .ToList();

            // Đảm bảo có đủ 12 tháng
            var fullResult = Enumerable.Range(1, 12)
                .Select(month => result.FirstOrDefault(x => x.Month == month) ?? new MonthlyRevenueDto { Month = month, TotalRevenue = 0 })
                .ToList();

            return fullResult;
        }

    }
}
