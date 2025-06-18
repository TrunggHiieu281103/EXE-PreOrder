using Application.DTOs.Order;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<BaseResponse<OrderResponseDto>>
    {
        public long UserId { get; set; }
     
        public decimal? ShippingFee { get; set; }
     
        public ICollection<OrderItemDto> Items { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseResponse<OrderResponseDto>>
    {
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IUserAddressRepositoryAsync _userAddressRepository;
        private readonly IPaymentRepositoryAsync _paymentRepository;
        private readonly IConnectionMultiplexer _redis;
        private readonly IProductRepositoryAsync _productRepository;

        public CreateOrderCommandHandler(
            IOrderRepositoryAsync orderRepository,
            IUserRepositoryAsync userRepository,
            IUserAddressRepositoryAsync userAddressRepository,
            IPaymentRepositoryAsync paymentRepository,
            IProductRepositoryAsync productRepository,
            IConnectionMultiplexer redis)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _userAddressRepository = userAddressRepository;
            _paymentRepository = paymentRepository;
            _productRepository = productRepository;
            _redis = redis;
        }

        public async Task<BaseResponse<OrderResponseDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdWithAddressAsync(request.UserId);
            if (user == null)
                throw new ApiException("User not found");

            var userAddress = await _userAddressRepository.GetDefaultAddressByUserIdAsync(request.UserId);
            if (userAddress == null)
                throw new ApiException("Default address not found");

            // Check stock availability
            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new ApiException($"Product with ID {item.ProductId} was not found.");

                if (product.IsPreOrder)
                    throw new ApiException($"Cannot order preorder product");

                if (product.StockQuantity < item.Quantity)
                    throw new ApiException($"Not enough quantity for product '{product.ProductName}'. Available: {product.StockQuantity}, requested: {item.Quantity}.");
            }

            var totalProductPrice = request.Items?.Sum(i => i.TotalPrice) ?? 0;
            var shipping = request.ShippingFee ?? 0;
            var finalAmount = totalProductPrice + shipping;

            if (request.PaymentType == PaymentTypeEnum.VNPAY)
            {
                // Store to Redis instead of DB
                var redis = _redis.GetDatabase();
                var redisOrderId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var redisKey = $"vnpay_order_{redisOrderId}";

                var redisOrder = new VnpayRedisOrderDto
                {
                    TempOrderId = redisOrderId,
                    UserId = request.UserId,
                    UserAddressId = userAddress.Id,
                    IsPreorder = false,
                    DepositPrice = 0,
                    ShippingFee = shipping,
                    TotalPrice = totalProductPrice,
                    Items = request.Items.ToList()
                };

                var json = JsonConvert.SerializeObject(redisOrder);
                await redis.StringSetAsync(redisKey, json, TimeSpan.FromMinutes(15)); // optional expiration

                return new BaseResponse<OrderResponseDto>(new OrderResponseDto
                {
                    OrderId = redisOrderId,
                    VnpayData = redisOrder
                }, "Order cached in Redis. Proceed to VNPAY.");
            }

            // == Handle COD normally ==
            var order = new Domain.Entities.Orders
            {
                UserId = request.UserId,
                UserAddressId = userAddress.Id,
                Status = OrderStatusEnum.CONFIRM.ToString(),
                IsPreorder = false,
                DepositPrice = 0,
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

            // Update stock
            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (!product.IsPreOrder)
                {
                    product.StockQuantity -= item.Quantity;
                    await _productRepository.UpdateAsync(product);
                }
            }

            var payment = new Payments
            {
                OrderId = order.Id,
                PaymentCode = $"PAY-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                PaymentType = request.PaymentType.ToString(),
                Content = $"Pay for orderId: {order.Id}",
                Amount = finalAmount,
                PaymentStatus = PaymentStatusEnum.PENDING.ToString()
            };

            await _paymentRepository.AddAsync(payment);

            return new BaseResponse<OrderResponseDto>(new OrderResponseDto
            {
                OrderId = order.Id,
                VnpayData = null
            }, "COD Order and payment created successfully.");
        }
    }
}
