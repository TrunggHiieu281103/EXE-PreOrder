using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class OrderProducts
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal? DepositPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Version { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
