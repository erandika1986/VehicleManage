using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VehicleTracker.Model
{
    public enum DailyBeatStatus
    {
        [Description("Planned")]
        Planned=1,

        [Description("Started")]
        Started =2,

        [Description("Completed")]
        Completed =3,

        [Description("Partialy Completed")]
        PartialyCompleted =4,

        [Description("Cancel")]
        Cancel =5
    }
}
