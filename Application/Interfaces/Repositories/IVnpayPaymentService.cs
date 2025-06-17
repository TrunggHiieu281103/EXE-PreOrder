using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;

namespace Application.Interfaces.Repositories
{
    public interface IVnpayPaymentService
    {

        string CreatePaymentUrl(decimal amount, string orderDescription, long orderId, BankCode bankCode);
        Task<PaymentResult> HandleVnpayCallback(IQueryCollection queryParams);
    }
}
