using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class Payments
    {
        public long Id { get; set; }
        public string PaymentCode { get; set; }
        public long OrderId { get; set; }
        public string PaymentType { get; set; }
        public string Content { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public int Version { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
