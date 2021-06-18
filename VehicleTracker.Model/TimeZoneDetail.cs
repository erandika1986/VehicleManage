using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class TimeZoneDetail
    {
        public TimeZoneDetail()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string TimeZoneId { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
