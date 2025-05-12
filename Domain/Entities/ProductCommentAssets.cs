using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProductCommentAssets : BaseEntity
    {
        public long Id { get; set; }
        public string MediaKey { get; set; }
        public long ProductCommentId { get; set; }
        public string PublicId { get; set; }
        public virtual ProductComments ProductComment { get; set; }

    }
}
