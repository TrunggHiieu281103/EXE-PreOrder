using Application.DTOs.Order;
using Application.Enums;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PreOrder.Commands.CreatePreOrder
{
    public class CreatePreOrderCommand : IRequest<BaseResponse<long>>
    {
        public long UserId { get; set; }
        public decimal? ShippingFee { get; set; }
        public bool IsPreorder { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        //public PaymentTypeEnum PaymentType { get; set; }

    }

    public class CreatePreOrderCommandHandler : IRequestHandler<CreatePreOrderCommand, BaseResponse<long>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IUserAddressRepositoryAsync _userAddressRepository;
        private readonly IPaymentRepositoryAsync _paymentRepository;
        public CreatePreOrderCommandHandler(
            IOrderRepositoryAsync orderRepository,
            IUserRepositoryAsync userRepository,
            IUserAddressRepositoryAsync userAddressRepository,
            IPaymentRepositoryAsync paymentRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _userAddressRepository = userAddressRepository;
            _paymentRepository = paymentRepository;
        }
        public async Task<BaseResponse<long>> Handle(CreatePreOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdWithAddressAsync(request.UserId);
            if (user == null)
                return new BaseResponse<long>("User not found");
            var userAddress = await _userAddressRepository.GetDefaultAddressByUserIdAsync(request.UserId);
            if (userAddress == null)
                return new BaseResponse<long>("Default address not found");

            var totalProductPrice = request.Items?.Sum(i => i.TotalPrice) ?? 0;
            var shipping = request.ShippingFee ?? 0;
            var deposit = totalProductPrice * 0.3m;
            var finalPaymentAmount = totalProductPrice + shipping - deposit;
            
            var order = new Domain.Entities.Orders
            {
                UserId = request.UserId,
                UserAddressId = userAddress.Id,
                Status = OrderStatusEnum.PENDING.ToString(),
                IsPreorder = true,
                DepositPrice = deposit,
                ShippingFee = request.ShippingFee,
                TotalPrice = totalProductPrice,
                OrderProducts = request.Items.Select(i => new OrderProducts
                {
                    ProductId = i.ProductId,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
            await _orderRepository.AddAsync(order);

            var payment = new Payments
            {
                OrderId = order.Id,
                PaymentCode = $"PAY-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                PaymentType = PaymentTypeEnum.VNPAY.ToString(),
                Content = $"Deposit for preorder OrderId: {order.Id}",
                Amount = deposit,
                PaymentStatus = PaymentStatusEnum.PENDING.ToString()
            };

            await _paymentRepository.AddAsync(payment);

            return new BaseResponse<long>(order.Id, "Order and payment created successfully.");

        }
    }
}
