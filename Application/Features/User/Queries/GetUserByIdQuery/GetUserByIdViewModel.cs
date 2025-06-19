using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries.GetUserByIdQuery
{
    public class GetUserByIdViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? AvatarKey { get; set; } = "samples/man-portrait";
        public string? AvatarPublicId { get; set; } = "samples/man-portrait";
        public string Phone { get; set; }
        public long? DateOfBirth { get; set; } = 0;
        public bool IsFirstLogin { get; set; }
        public bool IsEnableTwoFactor { get; set; }
        public int Version { get; set; }
        public bool IsActive { get; set; } 
        public long CreatedAt { get; set; } 
        public long UpdatedAt { get; set; } 
    }
}
