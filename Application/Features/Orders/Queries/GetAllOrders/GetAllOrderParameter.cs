using Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrderParameter : RequestParameter
    {
        public string? Email { get; set; }
    } 
    
}
