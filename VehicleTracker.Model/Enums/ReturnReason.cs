using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Model.Enums
{
  public enum ReturnReason
  {
    [Description("Expired")]
    Expired =1,
    [Description("Damaged")]
    Damaged =2,
    [Description("No Longer Needed")]
    NoLongerNeeded =3,
    [Description("Deliver Wrong Product")]
    ShippedWrongProduct =4,
    [Description("Incorrect Sales Order")]
    IncorrectSalesOrder =5,
    [Description("Other")]
    Other =6
  }
}
