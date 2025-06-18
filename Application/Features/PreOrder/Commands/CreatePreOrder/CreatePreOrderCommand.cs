using Application.DTOs.Order;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PreOrder.Commands.CreatePreOrder
{
    public class CreatePreOrderCommand : IRequest<BaseResponse<VnpayRedisOrderDto>>
    {
        public long UserId { get; set; }
        public decimal? ShippingFee { get; set; }
        //public bool IsPreorder { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        //public PaymentTypeEnum PaymentType { get; set; }

    }

    public class CreatePreOrderCommandHandler : IRequestHandler<CreatePreOrderCommand, BaseResponse<VnpayRedisOrderDto>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IUserAddressRepositoryAsync _userAddressRepository;
        private readonly IPaymentRepositoryAsync _paymentRepository;
        private readonly IProductRepositoryAsync _productRepositoryAsync;
        private readonly IConnectionMultiplexer _redis;

        public CreatePreOrderCommandHandler(
            IOrderRepositoryAsync orderRepository,
            IUserRepositoryAsync userRepository,
            IUserAddressRepositoryAsync userAddressRepository,
            IProductRepositoryAsync productRepositoryAsync,
            IPaymentRepositoryAsync paymentRepository,
            IConnectionMultiplexer redis)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _userAddressRepository = userAddressRepository;
            _paymentRepository = paymentRepository;
            _productRepositoryAsync = productRepositoryAsync;
            _redis = redis;
        }
        public async Task<BaseResponse<VnpayRedisOrderDto>> Handle(CreatePreOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdWithAddressAsync(request.UserId);
            if (user == null)
                throw new ApiException("User not found");

            var userAddress = await _userAddressRepository.GetDefaultAddressByUserIdAsync(request.UserId);
            if (userAddress == null)
                throw new ApiException("Default address not found");

            foreach (var item in request.Items)
            {
                var product = await _productRepositoryAsync.GetProductByIdAsync(item.ProductId);
                if (product == null)
                    throw new ApiException($"Product {item.ProductId} not found");

                if (!product.IsPreOrder)
                    throw new ApiException($"Product {product.ProductName} is not for preorder");

                if (product.StockQuantity < item.Quantity)
                    throw new ApiException($"Out of stock for product {product.ProductName}");
            }

            var totalProductPrice = request.Items.Sum(i => i.TotalPrice);
            var shipping = request.ShippingFee ?? 0;
            var deposit = totalProductPrice * 0.3m;
            var finalPaymentAmount = deposit;

            // Lưu tạm đơn hàng vào Redis
            var redisKey = $"vnpay_order_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
            var redisOrder = new VnpayRedisOrderDto
            {
                TempOrderId = long.Parse(redisKey.Split('_')[2]),
                UserId = request.UserId,
                UserAddressId = userAddress.Id,
                IsPreorder = true,
                DepositPrice = deposit,
                ShippingFee = shipping,
                TotalPrice = totalProductPrice,
                Items = request.Items.ToList()
            };

            var jsonData = JsonConvert.SerializeObject(redisOrder);
            var redis = _redis.GetDatabase();
            await redis.StringSetAsync(redisKey, jsonData, TimeSpan.FromMinutes(15)); // hết hạn 15 phút

            return new BaseResponse<VnpayRedisOrderDto>(redisOrder, "Preorder saved to Redis. Proceed to VNPAY.");
        }

    }
}
