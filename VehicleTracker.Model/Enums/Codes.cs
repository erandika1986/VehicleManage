using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VehicleTracker.Model.Enums
{
    public enum Codes
    {
        [Description("Break Oil Code")]
        BreakOilCode=1,
        [Description("Differenial Oil Code")]
        DifferenialOilCode =2,
        [Description("Engin Coolant Code")]
        EnginCoolantCode =3,
        [Description("Engine Oil Code")]
        EngineOilCode =4,
        [Description("Gear Box Oil Code")]
        GearBoxOilCode =5,
        [Description("Power Streering Oil Code")]
        PowerStreeringOilCode =6,

    }
}
