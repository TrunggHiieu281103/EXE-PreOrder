using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class RefreshTokens : BaseEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string RefreshToken { get; set; }
        public virtual Users User { get; set; }

    }
}
