using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.GetTotalOrders
{
    public class GetTotalOrdersViewModel
    {
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int SuccessOrders { get; set; }
        public int Orders { get; set; }
        public int PreOrders { get; set; }

    }

}
