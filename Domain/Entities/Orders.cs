using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Orders : BaseEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long UserAddress { get; set; }
        public string Status { get; set; }
        public decimal? DepositPrice { get; set; }
        public decimal? ShippingFee { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool IsPreorder { get; set; }
        public virtual Users User { get; set; }
        public virtual UserAddresses Address { get; set; }
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
        public virtual Shippings Shipping { get; set; }
        public virtual ICollection<ProductComments> ProductComments { get; set; }

    }
}
