using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleEngineOilMilageViewModel
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public decimal NextOilChangeMilage { get; set; }
        public decimal? ActualOilChangeMilage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
