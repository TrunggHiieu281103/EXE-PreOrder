using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Shippings : BaseEntity
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public string CarrierName { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public long? EstimatedDeliveryAt { get; set; }
        public long? ShippedAt { get; set; }
        public long? DeliveredAt { get; set; }
        public virtual Orders Order { get; set; }

    }
}
