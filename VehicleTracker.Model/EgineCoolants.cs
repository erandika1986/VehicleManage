using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class EgineCoolants
    {
        public EgineCoolants()
        {
            VehicleType = new HashSet<VehicleType>();
        }

        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<VehicleType> VehicleType { get; set; }
    }
}
