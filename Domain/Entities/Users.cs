using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Users : BaseEntity
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
        public virtual ICollection<Orders>? Orders { get; set; }
        public virtual ICollection<ProductComments>? ProductComments { get; set; }
        public virtual ICollection<UserAddresses>? UserAddresses { get; set; }
        public virtual ICollection<UserRoles>? UserRoles { get; set; }
        public virtual ICollection<RefreshTokens>? RefreshTokens { get; set; }

    }
}
