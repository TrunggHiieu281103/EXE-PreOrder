using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Users
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string AvatarKey { get; set; }
        public string AvatarPublicId { get; set; }
        public string Phone { get; set; }
        public long DateOfBirth { get; set; }
        public bool? IsFirstLogin { get; set; }
        public bool? IsEnableTwoFactor { get; set; }
        public bool? IsActive { get; set; }
        public int Version { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
