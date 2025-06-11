using Application.Features.Categories.Queries.GetCategoryById;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<BaseResponse<GetOrderByIdViewModel>>
    {
        public long Id { get; set; }
        public GetOrderByIdQuery(long id)
        {
            Id = id;
        }

        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, BaseResponse<GetOrderByIdViewModel>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IMapper _mapper;
            public GetOrderByIdQueryHandler(IOrderRepositoryAsync orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }
            public async Task<BaseResponse<GetOrderByIdViewModel>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.GetOrderByIdAsync(request.Id);
                if (order == null)
                    return new BaseResponse<GetOrderByIdViewModel>(null, $"Order with Id {request.Id} not found.");

                var viewModel = _mapper.Map<GetOrderByIdViewModel>(order);
                return new BaseResponse<GetOrderByIdViewModel>(viewModel, "Order retrieved successfully.");
            }

        }
    }
}
