using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;

namespace VehicleTracker.ViewModel.VehicleBeat
{
    public class DailyVehicleBeatViewModel
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public string VehicleNumber { get; set; }

        public long RouteId { get; set; }
        public string Route { get; set; }

        public DateTime Date { get; set; }
        public decimal StartingMilage { get; set; }
        public decimal? EndMilage { get; set; }
        public DailyBeatStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
