using Application.DTOs.DashBoard;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.PreOrder.Queries.GetAllPreOrders;
using Application.Repository;
using Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepositoryAsync : IGenericRepositoryAsync<Domain.Entities.Orders>
    {
        Task<IReadOnlyList<Domain.Entities.Orders>> GetOrderPagedResponseAsync(GetAllOrderParameter filter);
        Task<Domain.Entities.Orders> GetOrderByIdAsync(long id);
        Task<IReadOnlyList<Domain.Entities.Orders>> GetOrderPagedResponseByUserIdAsync(long userId, int pageNumber, int pageSize);
        Task<IReadOnlyList<Domain.Entities.Orders>> GetUserOrdersAsync(long userId, int pageNumber, int pageSize, bool? isPreOrder);
        Task<IReadOnlyList<Domain.Entities.Orders>> GetPreOrderPagedResponseAsync(GetAllPreOrderParameter filter);
        Task<int> CountAllOrdersAsync();
        Task<int> CountOrdersByPaymentStatusAsync(string status);
        Task<int> CountOrdersByTypeAsync(bool isPreorder);
        Task<decimal> GetTotalRevenueAsync();
        Task<List<Orders>> GetOrdersWithPaymentsAsync(Expression<Func<Orders, bool>> predicate);
        Task<List<MonthlyRevenueDto>> GetMonthlyRevenueAsync(int year);
    }
}
