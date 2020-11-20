using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class BreakOilCodes
    {
        public BreakOilCodes()
        {
            VehicleType = new HashSet<VehicleType>();
        }

        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<VehicleType> VehicleType { get; set; }
    }
}
