using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Model.Enums
{
  public enum ProductInventoryOrderActionType
  {
    [Description("Sales Order")]
    SalesOrder=1,
    [Description("Product Return")]
    ProductReturn =2
  }
}
