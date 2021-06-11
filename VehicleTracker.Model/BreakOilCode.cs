﻿using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class BreakOilCode
    {
        public BreakOilCode()
        {
            VehicleTypes = new HashSet<VehicleType>();
        }

        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<VehicleType> VehicleTypes { get; set; }
    }
}
