using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class Route
    {
        public Route()
        {
            DailyVehicleBeat = new HashSet<DailyVehicleBeat>();
        }

        public long Id { get; set; }
        public string RouteCode { get; set; }
        public string StartFrom { get; set; }
        public string EndFrom { get; set; }
        public decimal? TotalDistance { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeat { get; set; }
    }
}
