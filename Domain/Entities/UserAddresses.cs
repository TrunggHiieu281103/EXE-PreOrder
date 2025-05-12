using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class UserAddresses : BaseEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetail { get; set; }
        public bool IsDefault { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Orders> Orders { get; set; } 

    }
}
