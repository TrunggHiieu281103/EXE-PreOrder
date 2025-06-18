using Application.Features.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPendingOrderService
    {
        Task StorePendingOrderAsync(string key, CreateOrderCommand order);
        Task<CreateOrderCommand> GetPendingOrderAsync(string key);
        Task RemovePendingOrderAsync(string key);
    }
}
