using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleEmissiontTest
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public DateTime EmissiontTestDate { get; set; }
        public DateTime NextEmissiontTestDate { get; set; }
        public string Note { get; set; }
        public string Attachment { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
