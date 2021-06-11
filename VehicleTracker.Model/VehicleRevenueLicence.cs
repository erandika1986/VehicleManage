using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class VehicleRevenueLicence
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public DateTime RevenueLicenceDate { get; set; }
        public DateTime ValidTill { get; set; }
        public string Note { get; set; }
        public string Attachment { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
