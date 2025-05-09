using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class Shippings
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
        public int Version { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
