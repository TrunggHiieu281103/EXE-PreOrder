using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class OrderResponseDto
    {
        public long OrderId { get; set; }                  // dùng cho COD
        public VnpayRedisOrderDto? VnpayData { get; set; } // dùng cho VNPAY
    }
}
