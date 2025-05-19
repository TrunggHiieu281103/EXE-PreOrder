using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAllProduct
{
    public class ProductAssetViewModel
    {
        public string MediaKey { get; set; }
        public string PublicId { get; set; }
        public int MyProperty { get; set; }
        // Tùy nếu bạn có URL từ Cloudinary
        //public string ImageUrl => $"https://res.cloudinary.com/<ten_cloud>/image/upload/{PublicId}.jpg";
    }

}
