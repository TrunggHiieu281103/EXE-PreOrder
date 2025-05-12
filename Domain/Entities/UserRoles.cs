using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class UserRoles : BaseEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public virtual Users User { get; set; }
        public virtual Roles Role { get; set; }

    }
}
