using Application.DTOs.Order;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<BaseResponse<long>>
    {
        public long UserId { get; set; }
        //public string Status { get; set; }
        
        public decimal? DepositPrice { get; set; }
        public decimal? ShippingFee { get; set; }
        public bool IsPreorder { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }

        public decimal TotalPrice => Items?.Sum(i => i.TotalPrice) ?? 0;
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseResponse<long>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IUserAddressRepositoryAsync _userAddressRepository;

        public CreateOrderCommandHandler(IOrderRepositoryAsync orderRepository, IUserRepositoryAsync userRepositoryAsync, IUserAddressRepositoryAsync userAddressRepository)
        {
            _userRepository = userRepositoryAsync;
            _orderRepository = orderRepository;
            _userAddressRepository = userAddressRepository;
        }

        public async Task<BaseResponse<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdWithAddressAsync(request.UserId);
            if (user == null)
                return new BaseResponse<long>("User not found");

            var userAddress = _userAddressRepository.GetDefaultAddressByUserIdAsync(request.UserId);
            if (userAddress == null)
                return new BaseResponse<long>("Default address for user not found");

            var totalOrderPrice = request.TotalPrice;

            var order = new Domain.Entities.Orders
            {
                UserId = request.UserId,
                UserAddressId = userAddress.Id,
                Status = "CONFIRMED",
                IsPreorder = request.IsPreorder,
                DepositPrice = request.DepositPrice,
                ShippingFee = request.ShippingFee,
                TotalPrice = totalOrderPrice, // Nếu bạn có thuộc tính này trong entity Orders
                OrderProducts = request.Items.Select(i => new OrderProducts
                {
                    ProductId = i.ProductId,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _orderRepository.AddAsync(order);

            return new BaseResponse<long>(order.Id, "Order created successfully.");
        }

    }
}
