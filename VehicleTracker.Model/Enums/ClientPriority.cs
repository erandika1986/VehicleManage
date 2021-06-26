using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Model.Enums
{
    public enum ClientPriority
    {
        [Description("High Priority Client")]
        High=1,
        [Description("Medium Priority Client")]
        Medium =2,
        [Description("Low Priority Client")]
        Low =3
    }
}
