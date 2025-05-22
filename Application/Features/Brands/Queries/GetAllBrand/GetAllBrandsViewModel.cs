using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetAllBrand
{
    public class GetAllBrandsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}