using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Model.Enums
{
  public enum SalesOrderStatus
  {
    New=1,
    PlannedForDelivery=2,
    Shipped=3,
    Completed=4,
    OnHold=5,
    Cancelled=6
  }
}
