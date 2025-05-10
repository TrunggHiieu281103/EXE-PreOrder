using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Categories
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
