using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProductComments : BaseEntity
    {
        public long Id { get; set; }
        public double? Rating { get; set; }
        public string Comment { get; set; }
        public long? UserId { get; set; }
        public long? ProductId { get; set; }
        public long? OrderId { get; set; }
        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<ProductCommentAssets> ProductCommentAssets { get; set; }

    }
}
