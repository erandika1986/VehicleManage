using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleInsurance
    {
        public VehicleInsurance()
        {
            InverseParent = new HashSet<VehicleInsurance>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long VehicleId { get; set; }
        public DateTime NextInsuranceDate { get; set; }
        public DateTime? ActualInsuranceDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual VehicleInsurance Parent { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleInsurance> InverseParent { get; set; }
    }
}
