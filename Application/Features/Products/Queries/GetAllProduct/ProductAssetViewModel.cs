using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAllProduct
{
    public class ProductAssetViewModel
    {
        public long Id { get; set; }
        public string MediaKey { get; set; }
        public string PublicId { get; set; }
        public string ImageUrl { get; set; }
    }

}
