using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetOrderByUserId
{
    public class GetOrderByUserIdViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long UserAddressId { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public decimal? DepositPrice { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool IsPreorder { get; set; }
    }
}
