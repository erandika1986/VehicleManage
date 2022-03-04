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
        [Description("Pending")]
        Pending = 1,
        [Description("New")]
        New = 2,
        [Description("Released")]
        Released = 3,
        [Description("Received")]
        Received = 4,
        [Description("Canceled")]
        Canceled = 5,
        [Description("Closed")]
        Closed = 6
    }
}
