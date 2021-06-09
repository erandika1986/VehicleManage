using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleDifferentialOilChangeMilage
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public decimal DifferentialOilChangeMilage { get; set; }
        public decimal NextDifferentialOilChangeMilage { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
