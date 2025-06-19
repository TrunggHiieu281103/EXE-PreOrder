using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdViewModel
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}

