using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Model.Enums
{
    public enum SalesOrderStatus
    {
        Pending = 1,
        New = 2,
        PlannedForDelivery = 3,
        Shipped = 4,
        Completed = 5,
        OnHold = 6,
        Cancelled = 7
    }
}
