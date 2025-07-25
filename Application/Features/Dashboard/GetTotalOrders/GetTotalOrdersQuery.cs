using Application.Features.Dashboard.TotalUser;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.GetTotalOrders
{
    public class GetTotalOrdersQuery : IRequest<BaseResponse<GetTotalOrdersViewModel>>
    {


        public class GetTotalOrdersHandler : IRequestHandler<GetTotalOrdersQuery, BaseResponse<GetTotalOrdersViewModel>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;

            public GetTotalOrdersHandler(IOrderRepositoryAsync orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<BaseResponse<GetTotalOrdersViewModel>> Handle(GetTotalOrdersQuery request, CancellationToken cancellationToken)
            {
                var total = await _orderRepository.CountAllOrdersAsync();
                var pending = await _orderRepository.CountOrdersByPaymentStatusAsync("PENDING");
                var success = await _orderRepository.CountOrdersByPaymentStatusAsync("SUCCESS");
                var order = await _orderRepository.CountOrdersByTypeAsync(false);
                var preorder = await _orderRepository.CountOrdersByTypeAsync(true);

                var result = new GetTotalOrdersViewModel
                {
                    TotalOrders = total,
                    PendingOrders = pending,
                    SuccessOrders = success,
                    Orders = order,
                    PreOrders = preorder
                };

                return new BaseResponse<GetTotalOrdersViewModel>(result, "Get total order success");
            }
        }
    }

    
}
