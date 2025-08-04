using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PreOrder.Queries.GetAllPreOrders
{
    public class GetAllPreOrderQuery : IRequest<PageResponse<IEnumerable<GetAllPreOrderViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserEmail { get; set; }
        public class GetAllPreOrderQueryHandler : IRequestHandler<GetAllPreOrderQuery, PageResponse<IEnumerable<GetAllPreOrderViewModel>>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;
            public GetAllPreOrderQueryHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
            {
                _mapper = mapper;
                _orderRepository = orderRepository;
            }
            public async Task<PageResponse<IEnumerable<GetAllPreOrderViewModel>>> Handle(GetAllPreOrderQuery request, CancellationToken cancellationToken)
            {
                var preOrderFilter = new GetAllPreOrderParameter
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    UserEmail = request.UserEmail
                };
                var (preOrders, totalItems) = await _orderRepository.GetPreOrderPagedResponseAsync(preOrderFilter);
                return new PageResponse<IEnumerable<GetAllPreOrderViewModel>>(
                    _mapper.Map<IEnumerable<GetAllPreOrderViewModel>>(preOrders),
                    request.PageNumber,
                    request.PageSize,
                    totalItems
                );
            }
        }
    }
}
