using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Model.Enums
{
  public enum ReturnProductStatus
  {
    [Description("Return To Inventory")]
    ReturnToInventory=1,
    [Description("Stored In Return Store")]
    StoredInReturnStore =2,
    [Description("Discarded")]
    Discarded =3
  }
}
