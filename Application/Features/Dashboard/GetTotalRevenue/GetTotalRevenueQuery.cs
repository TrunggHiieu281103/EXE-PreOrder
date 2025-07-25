using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.GetTotalRevenue
{
    public class GetTotalRevenueQuery : IRequest<BaseResponse<decimal>>
    {

        public class GetTotalRevenueQueryHandler : IRequestHandler<GetTotalRevenueQuery, BaseResponse<decimal>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;

            public GetTotalRevenueQueryHandler(IOrderRepositoryAsync orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<BaseResponse<decimal>> Handle(GetTotalRevenueQuery request, CancellationToken cancellationToken)
            {
                var result = await _orderRepository.GetTotalRevenueAsync();

                return new BaseResponse<decimal>(result, "Get total revenue success");
            }
        }
    }

    
}
