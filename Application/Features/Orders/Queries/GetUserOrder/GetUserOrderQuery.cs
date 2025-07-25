using Application.Features.Orders.Queries.GetOrderByUserId;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetUserOrder
{
    public class GetUserOrderQuery : IRequest<PageResponse<IEnumerable<GetOrderByUserIdViewModel>>>
    {
        public long UserId { get; private set; }
        public void SetUserId(long id) => UserId = id;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool? IsPreorder { get; set; }

        public class GetUserOrderQueryHandler : IRequestHandler<GetUserOrderQuery, PageResponse<IEnumerable<GetOrderByUserIdViewModel>>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;

            public GetUserOrderQueryHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }

            public async Task<PageResponse<IEnumerable<GetOrderByUserIdViewModel>>> Handle(GetUserOrderQuery request, CancellationToken cancellationToken)
            {
                var orders = await _orderRepository.GetUserOrdersAsync(request.UserId, request.PageNumber, request.PageSize, request.IsPreorder);

                var orderViewModels = _mapper.Map<IEnumerable<GetOrderByUserIdViewModel>>(orders);

                return new PageResponse<IEnumerable<GetOrderByUserIdViewModel>>(orderViewModels, request.PageNumber, request.PageSize);
            }
        }
    }
}
    
