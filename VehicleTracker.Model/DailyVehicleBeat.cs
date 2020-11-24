using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class DailyVehicleBeat
    {
        public DailyVehicleBeat()
        {
            DailyVehicleBeatOrders = new HashSet<DailyVehicleBeatOrders>();
        }

        public long Id { get; set; }
        public long VehicleId { get; set; }
        public long RouteId { get; set; }
        public DateTime Date { get; set; }
        public decimal? StartingMilage { get; set; }
        public decimal? EndMilage { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Route Route { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<DailyVehicleBeatOrders> DailyVehicleBeatOrders { get; set; }
    }
}
