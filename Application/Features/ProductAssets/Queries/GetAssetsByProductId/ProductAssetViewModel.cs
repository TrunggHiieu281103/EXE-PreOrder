using Application.Features.Products.Queries.GetAllProduct;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductAssets.Queries.GetAssetsByProductId
{
    public class ProductAssetViewModel : BaseEntity
    {
        public long Id { get; set; }
        public string MediaKey { get; set; }
        public string PublicId { get; set; }
        public string ImageUrl { get; set; }
    }
}
