using Application.DTOs.DashBoard;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.GetRevenueByDate
{
    public class GetMonthlyRevenueQuery : IRequest<BaseResponse<List<MonthlyRevenueDto>>>
    {
        public int Year { get; set; }

        public class GetMonthlyRevenueQueryHandler : IRequestHandler<GetMonthlyRevenueQuery, BaseResponse<List<MonthlyRevenueDto>>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;

            public GetMonthlyRevenueQueryHandler(IOrderRepositoryAsync orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<BaseResponse<List<MonthlyRevenueDto>>> Handle(GetMonthlyRevenueQuery request, CancellationToken cancellationToken)
            {
                var revenues = await _orderRepository.GetMonthlyRevenueAsync(request.Year);
                return new BaseResponse<List<MonthlyRevenueDto>>(revenues);
            }
        }
    }
}
