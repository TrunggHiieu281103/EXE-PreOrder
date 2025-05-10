using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProductAssets
    {
        public long Id { get; set; }
        public string MediaKey { get; set; }
        public long ProductId { get; set; }
        public string PublicId { get; set; }
        public bool? IsActive { get; set; }
        public int Version { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
