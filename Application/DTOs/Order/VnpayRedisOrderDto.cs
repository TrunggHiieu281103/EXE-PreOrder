using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class VnpayRedisOrderDto
    {
        public long TempOrderId { get; set; }
        public long UserId { get; set; }
        public long UserAddressId { get; set; }
        public bool IsPreorder { get; set; }
        public decimal DepositPrice { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
