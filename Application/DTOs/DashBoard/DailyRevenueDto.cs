using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.DashBoard
{
    public class DailyRevenueDto
    {
        public string Day { get; set; } // yyyy-MM-dd
        public decimal TotalRevenue { get; set; }
    }
}
