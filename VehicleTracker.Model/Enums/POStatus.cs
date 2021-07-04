using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Model.Enums
{
  public enum POStatus
  {
    [Description("New")]
    New=1,
    [Description("Released")]
    Released =2,
    [Description("Received")]
    Received =3,
    [Description("Canceled")]
    Canceled =4,
    [Description("Closed")]
    Closed =5
  }
}
