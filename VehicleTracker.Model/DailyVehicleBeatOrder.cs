using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class DailyVehicleBeatOrder
    {
        public int Id { get; set; }
        public long DailyVehicleBeatId { get; set; }
        public long OrderId { get; set; }

        public virtual DailyVehicleBeat DailyVehicleBeat { get; set; }
        public virtual SalesOrder Order { get; set; }
    }
}
