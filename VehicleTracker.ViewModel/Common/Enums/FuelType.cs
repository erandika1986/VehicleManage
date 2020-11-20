using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VehicleTracker.ViewModel.Common.Enums
{
    public enum FuelType
    {
        [Description("Octain 92")]
        Octain92 = 1,
        [Description("95 Octane EURO 4")]
        Octain95 = 2,
        [Description("Auto Diesel")]
        AutoDiesel = 3,
        [Description("Lanka Super Diesel")]
        LankaSuperDiesel = 4
    }
}
