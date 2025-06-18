using Application.Enums;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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

        public VnpayPaymentService(
    IVnpay vnpay,
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor,
    IPaymentRepositoryAsync paymentRepository,
    IProductRepositoryAsync productRepositoryAsync,
    IOrderRepositoryAsync orderRepository)
        {
            _vnpay = vnpay;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepositoryAsync;

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

            var payment = await _paymentRepository.GetByOrderIdAsync(orderId);
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (payment == null || order == null)
                return result;

            if (result.IsSuccess)
            {
                payment.PaymentStatus = PaymentStatusEnum.SUCCESS.ToString();
                order.Status = OrderStatusEnum.CONFIRM.ToString();

                await _paymentRepository.UpdateAsync(payment);
                await _orderRepository.UpdateAsync(order);

                // Trừ tồn kho tại đây nếu không phải PreOrder
                foreach (var orderProduct in order.OrderProducts)
                {
                    var product = await _productRepository.GetByIdAsync(orderProduct.ProductId);
          
                        product.StockQuantity -= orderProduct.Quantity;
                        await _productRepository.UpdateAsync(product); 
                }
            }
            else
            {
                payment.PaymentStatus = PaymentStatusEnum.FAILED.ToString();
                await _paymentRepository.UpdateAsync(payment);
            }

            return result;
        }



    }
}
