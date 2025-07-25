using Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetUserOrder
{
    public class GetUserOrderParameter : RequestParameter
    {
        public bool? IsPreorder { get; set; }
    }
}
