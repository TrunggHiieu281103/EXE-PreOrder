using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class Orders
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long UserAddress { get; set; }
        public string Status { get; set; }
        public decimal? DepositPrice { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool IsPreorder { get; set; }
        public int Version { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
