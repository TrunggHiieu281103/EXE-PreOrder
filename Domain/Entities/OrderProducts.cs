using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class OrderProducts : BaseEntity
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal? DepositPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }

    }
}
