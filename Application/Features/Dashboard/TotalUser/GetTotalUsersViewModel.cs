using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.TotalUser
{
    public class GetTotalUsersViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }

        // Có thể thêm các thông tin khác như:
        public int NewUsersThisMonth { get; set; }
        public int AdminCount { get; set; }
    }

    
}
