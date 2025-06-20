using Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PreOrder.Queries.GetAllPreOrders
{
    public class GetAllPreOrderParameter : RequestParameter
    {
        public string? UserEmail { get; set; }

    }
}
