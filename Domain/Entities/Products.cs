using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Products
    {
        public long Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public int? StockQuantity { get; set; }
        public string ProductDetails { get; set; }
        public decimal Price { get; set; }
        public long? OpenedAt { get; set; }
        public bool IsPreOrder { get; set; }
        public int Version { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
