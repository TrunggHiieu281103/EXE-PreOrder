using Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries.GetAllUser
{
    public class GetAllUserParameter : RequestParameter
    {
        public string? Email { get; set; }

    }
}
