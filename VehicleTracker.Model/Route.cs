using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class Route
    {
        public Route()
        {
            Clients = new HashSet<Client>();
            DailyVehicleBeats = new HashSet<DailyVehicleBeat>();
        }

        public long Id { get; set; }
        public string RouteCode { get; set; }
        public string StartFrom { get; set; }
        public string EndFrom { get; set; }
        public decimal? TotalDistance { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeats { get; set; }
    }
}
