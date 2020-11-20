using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleRevenueLicence
    {
        public VehicleRevenueLicence()
        {
            InverseParent = new HashSet<VehicleRevenueLicence>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long VehicleId { get; set; }
        public DateTime NextRevenueLicenceDate { get; set; }
        public DateTime? ActualRevenueLicenceDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual VehicleRevenueLicence Parent { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleRevenueLicence> InverseParent { get; set; }
    }
}
