using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.PreOrder.Queries.GetAllPreOrders;
using Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepositoryAsync : IGenericRepositoryAsync<Domain.Entities.Orders>
    {
        Task<IReadOnlyList<Domain.Entities.Orders>> GetOrderPagedResponseAsync(GetAllOrderParameter filter);
        Task<Domain.Entities.Orders> GetOrderByIdAsync(long id);
        Task<IReadOnlyList<Domain.Entities.Orders>> GetOrderPagedResponseByUserIdAsync(long userId, int pageNumber, int pageSize);
        Task<IReadOnlyList<Domain.Entities.Orders>> GetPreOrderPagedResponseAsync(GetAllPreOrderParameter filter);
    }
}
