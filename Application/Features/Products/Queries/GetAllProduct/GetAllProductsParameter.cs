using Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductsParameter : RequestParameter
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public bool? IsPreOrder { get; set; }
        public string? Type { get; set; }
        public string? Size { get; set; }
    }
}
