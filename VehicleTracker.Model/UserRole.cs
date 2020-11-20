using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class UserRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
