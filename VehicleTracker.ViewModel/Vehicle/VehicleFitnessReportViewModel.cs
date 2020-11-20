using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleFitnessReportViewModel
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public DateTime NextFitnessReportDate { get; set; }
        public DateTime? ActualFitnessReportDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
