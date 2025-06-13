using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Payment
{
    public class PaymentDto
    {
        public long Id { get; set; }
        public string PaymentCode { get; set; }
        public string PaymentType { get; set; }
        public string Content { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
    }
}
