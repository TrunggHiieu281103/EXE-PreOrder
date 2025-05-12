using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProductAssets : BaseEntity
    {
        public long Id { get; set; }
        public string MediaKey { get; set; }
        public long ProductId { get; set; }
        public string PublicId { get; set; }
        public virtual Products Product { get; set; }

    }
}
