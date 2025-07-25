using Application.Enums;
using Application.Exceptions;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<BaseResponse<long>>
    {
        public long UserId { get; private set; }
        public long Id { get; private set; }

        public UpdateOrderCommand(long id, long userId)
        {
            Id = id;
            UserId = userId;
        }


        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, BaseResponse<long>>
        {
            private readonly IOrderRepositoryAsync _orderRepository;
            private readonly IPaymentRepositoryAsync _paymentRepository;

            public UpdateOrderCommandHandler(IOrderRepositoryAsync orderRepositoryAsync, IPaymentRepositoryAsync paymentRepositoryAsync)
            {
                _orderRepository = orderRepositoryAsync;
                _paymentRepository = paymentRepositoryAsync;
            }
            public async Task<BaseResponse<long>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
            {
                var order =await _orderRepository.GetOrderByIdAsync(command.Id);

                if (order == null)
                {
                    throw new ApiException($"Order with ID {command.Id} not found.");
                }
                else
                { 
                    order.Status = OrderStatusEnum.COMPLETED.ToString();
                    var payment = await _paymentRepository.GetByOrderIdAsync(order.Id);
                    payment.PaymentStatus = PaymentStatusEnum.SUCCESS.ToString();

                    await _paymentRepository.UpdateAsync(payment);
                    await _orderRepository.UpdateAsync(order);
                    return new BaseResponse<long>(order.Id, "Order updated successfully.");
                }
            }
        }
    }
}
