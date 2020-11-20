using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
