using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleFuelFilterMilageViewModel
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public decimal NextFuelFilterChangeMilage { get; set; }
        public decimal? ActualFuelFilterChangeMilage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
