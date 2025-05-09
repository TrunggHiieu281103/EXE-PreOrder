using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class Roles
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public bool? IsActive { get; set; }
        public int Version { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
