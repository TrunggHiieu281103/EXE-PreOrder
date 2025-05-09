using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class UserAddresses
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetail { get; set; }
        public bool IsDefault { get; set; }
        public bool? IsActive { get; set; }
        public int Version { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
