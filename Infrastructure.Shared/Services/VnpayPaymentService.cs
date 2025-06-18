using Application.DTOs.Order;
using Application.Enums;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;

namespace Infrastructure.Shared.Services
{
    public class VnpayPaymentService : IVnpayPaymentService
    {
        private readonly IVnpay _vnpay;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPaymentRepositoryAsync _paymentRepository;
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public VnpayPaymentService(
    IVnpay vnpay,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor,
    IPaymentRepositoryAsync paymentRepository,
    IProductRepositoryAsync productRepositoryAsync,
    IOrderRepositoryAsync orderRepository,
            IConnectionMultiplexer redis)
        {
            _vnpay = vnpay;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepositoryAsync;
            _redis = redis;
            _db = _redis.GetDatabase();

            _vnpay.Initialize(
                _configuration["Vnpay:TmnCode"],
                _configuration["Vnpay:HashSecret"],
                _configuration["Vnpay:BaseUrl"],
                _configuration["Vnpay:ReturnUrl"]
            );
        }

        public string CreatePaymentUrl(decimal amount, string orderDescription, long orderId, BankCode bankCode)
        {
            try
            {
                double moneyToPay = (double)amount;

                // Get client IP address
                var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "127.0.0.1";

                var request = new PaymentRequest
                {
                    PaymentId = orderId , // hoặc: DateTimeOffset.Now.ToUnixTimeMilliseconds()
                    Money = moneyToPay,
                    Description = orderDescription,
                    IpAddress = ipAddress,
                    BankCode = bankCode,
                    CreatedDate = DateTime.Now,
                    Currency = Currency.VND,
                    Language = DisplayLanguage.Vietnamese
                };

                var paymentUrl = _vnpay.GetPaymentUrl(request);
                return paymentUrl;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentResult> HandleVnpayCallback(IQueryCollection queryParams)
        {
            var result = _vnpay.GetPaymentResult(queryParams);
            var orderId = result.PaymentId;
            var redisKey = $"vnpay_order_{orderId}";

            if (!result.IsSuccess)
            {
                await _db.KeyDeleteAsync(redisKey);
                return result;
            }

            // Lấy dữ liệu đơn hàng từ Redis
            var jsonData = await _db.StringGetAsync(redisKey);
            if (jsonData.IsNullOrEmpty)
                return result;

            var redisOrderData = JsonConvert.DeserializeObject<VnpayRedisOrderDto>(jsonData!);
            if (redisOrderData == null)
                return result;

            // Tạo đơn hàng chính thức
            var order = new Domain.Entities.Orders
            {
                UserId = redisOrderData.UserId,
                UserAddressId = redisOrderData.UserAddressId,
                IsPreorder = redisOrderData.IsPreorder,
                Status = OrderStatusEnum.CONFIRM.ToString(),
                DepositPrice = redisOrderData.DepositPrice,
                ShippingFee = redisOrderData.ShippingFee,
                TotalPrice = redisOrderData.TotalPrice,
                OrderProducts = redisOrderData.Items.Select(i => new OrderProducts
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            await _orderRepository.AddAsync(order);

            var payment = new Payments
            {
                OrderId = order.Id,
                PaymentCode = $"PAY-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                PaymentType = PaymentTypeEnum.VNPAY.ToString(),
                Amount = redisOrderData.IsPreorder
                    ? redisOrderData.DepositPrice
                    : redisOrderData.TotalPrice + redisOrderData.ShippingFee,
                Content = $"Payment for orderId: {order.Id}",
                PaymentStatus = PaymentStatusEnum.SUCCESS.ToString()
            };

            await _paymentRepository.AddAsync(payment);

            // Trừ tồn kho nếu là PreOrder
            foreach (var item in redisOrderData.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                product.StockQuantity -= item.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            // Xoá dữ liệu Redis
            await _db.KeyDeleteAsync(redisKey);

            return result;
        }



    }
}
