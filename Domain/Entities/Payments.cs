using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Payments : BaseEntity
    {
        public long Id { get; set; }
        public string PaymentCode { get; set; }
        public long OrderId { get; set; }
        public string PaymentType { get; set; }
        public string Content { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public virtual Orders Order { get; set; }

    }
}
