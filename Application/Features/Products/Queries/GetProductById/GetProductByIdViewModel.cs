using Application.Features.ProductAssets.Queries.GetAssetsByProductId;
using Application.Features.Products.Queries.GetAllProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdViewModel
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
        public int Discount { get; set; }  // phần trăm, ví dụ: 10 = 10%
        public decimal DiscountedPrice { get; set; }  // giá sau khi áp dụng giảm
        public long? OpenedAt { get; set; }
        public bool IsPreOrder { get; set; }

        //Base entity property
        public int Version { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }

        //
        public ICollection<ProductAssetViewModel> ProductAssets { get; set; }
    }
}
