using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleFitnessReport
    {
        public VehicleFitnessReport()
        {
            InverseParent = new HashSet<VehicleFitnessReport>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long VehicleId { get; set; }
        public DateTime NextFitnessReportDate { get; set; }
        public DateTime? ActualFitnessReportDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual VehicleFitnessReport Parent { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleFitnessReport> InverseParent { get; set; }
    }
}
