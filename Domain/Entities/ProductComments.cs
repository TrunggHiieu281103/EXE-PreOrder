using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProductComments
    {
        public long Id { get; set; }
        public double? Rating { get; set; }
        public string Comment { get; set; }
        public long? UserId { get; set; }
        public long? ProductId { get; set; }
        public long? OrderId { get; set; }
        public bool? IsActive { get; set; }
        public int Version { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
