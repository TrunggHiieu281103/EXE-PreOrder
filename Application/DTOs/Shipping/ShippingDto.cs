using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Shipping
{
    public class ShippingDto
    {
        public long Id { get; set; }
        public string TrackingNumber { get; set; }
        public string CarrierName { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public long? EstimatedDeliveryAt { get; set; }
        public long? ShippedAt { get; set; }
        public long? DeliveredAt { get; set; }
    }
}
