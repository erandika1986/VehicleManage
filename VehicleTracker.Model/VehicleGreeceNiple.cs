using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleGreeceNiple
    {
        public VehicleGreeceNiple()
        {
            InverseParent = new HashSet<VehicleGreeceNiple>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long VehicleId { get; set; }
        public DateTime NextGreeceNipleReplaceDate { get; set; }
        public DateTime? ActualGreeceNipleReplaceDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual VehicleGreeceNiple Parent { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<VehicleGreeceNiple> InverseParent { get; set; }
    }
}
