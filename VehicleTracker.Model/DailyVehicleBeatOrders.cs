using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class DailyVehicleBeatOrders
    {
        public int Id { get; set; }
        public long DailyVehicleBeatId { get; set; }
        public long OrderId { get; set; }

        public virtual DailyVehicleBeat DailyVehicleBeat { get; set; }
        public virtual Order Order { get; set; }
    }
}
