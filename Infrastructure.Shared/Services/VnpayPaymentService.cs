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

        public VnpayPaymentService(IVnpay vnpay, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _vnpay = vnpay;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

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

        public PaymentResult HandleVnpayCallback(IQueryCollection queryParams)
        {
            return _vnpay.GetPaymentResult(queryParams);
        }

        
    }
}
