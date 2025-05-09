using System;
using System.Collections.Generic;

namespace Persistence.Models
{
    public partial class ProductCommentAssets
    {
        public long Id { get; set; }
        public string MediaKey { get; set; }
        public long ProductCommentId { get; set; }
        public string PublicId { get; set; }
        public bool? IsActive { get; set; }
        public int Version { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
