using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleAirCleanerViewModel
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public decimal AirCleanerReplaceMilage { get; set; }
        public decimal NextAirCleanerReplaceMilage { get; set; }

        public DateTime? CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
