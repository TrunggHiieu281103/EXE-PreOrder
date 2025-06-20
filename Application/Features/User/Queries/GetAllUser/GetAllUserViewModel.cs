using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries.GetAllUserQuery
{
    public class GetAllUserViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? AvatarKey { get; set; } = "samples/man-portrait";
        public string? AvatarPublicId { get; set; } = "samples/man-portrait";
        public string Phone { get; set; }
        public long? DateOfBirth { get; set; } = 0;
        public ICollection<string> Roles { get; set; }

    }
}
