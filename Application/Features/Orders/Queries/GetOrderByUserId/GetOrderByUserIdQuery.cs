using Application.Features.Orders.Queries.GetOrderById;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetOrderByUserId
{
    public class GetOrderByUserIdQuery : IRequest<PageResponse<IEnumerable<GetOrderByUserIdViewModel>>>
    {
        public long UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetOrderByUserIdQuery(long userId, int pageNumber = 1, int pageSize = 10)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, PageResponse<IEnumerable<GetOrderByUserIdViewModel>>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;
            public GetOrderByUserIdQueryHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }
            public async Task<PageResponse<IEnumerable<GetOrderByUserIdViewModel>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
            {
                // Lấy tất cả đơn hàng của user
                var allOrders = await _orderRepository.GetOrderPagedResponseByUserIdAsync(request.UserId, request.PageNumber, request.PageSize);

                // Map sang ViewModel
                var viewModels = _mapper.Map<IEnumerable<GetOrderByUserIdViewModel>>(allOrders);

                return new PageResponse<IEnumerable<GetOrderByUserIdViewModel>>(viewModels, request.PageNumber, request.PageSize);
            }
        }
    }
       
    
}
