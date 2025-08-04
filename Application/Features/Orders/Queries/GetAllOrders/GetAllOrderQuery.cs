using Application.Features.Categories.Queries.GetAllCategory;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrderQuery : IRequest<PageResponse<IEnumerable<GetAllOrderViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Email { get; set; }

        public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, PageResponse<IEnumerable<GetAllOrderViewModel>>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;

            public GetAllOrderQueryHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
            {
                _mapper = mapper;
                _orderRepository = orderRepository;
            }
            public async Task<PageResponse<IEnumerable<GetAllOrderViewModel>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
            {
                var orderFilter = new GetAllOrderParameter
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    Email = request.Email
                };

                var (orders, totalItems) = await _orderRepository.GetOrderPagedResponseAsync(orderFilter);
                return new PageResponse<IEnumerable<GetAllOrderViewModel>>(
                    _mapper.Map<IEnumerable<GetAllOrderViewModel>>(orders),
                    request.PageNumber,
                    request.PageSize,
                    totalItems
                );
            }
        }
    }
}
