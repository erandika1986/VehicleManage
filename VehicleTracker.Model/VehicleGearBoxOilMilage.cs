using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleGearBoxOilMilage
    {
        public VehicleGearBoxOilMilage()
        {
            InverseParent = new HashSet<VehicleGearBoxOilMilage>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long VehicleId { get; set; }
        public decimal NextGearBoxOilChangeMilage { get; set; }
        public decimal? ActualGearBoxOilChangeMilage { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual VehicleGearBoxOilMilage Parent { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleGearBoxOilMilage> InverseParent { get; set; }
    }
}
