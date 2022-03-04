using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.Common.Enums
{
    public enum VehicleExpensesTypes
    {
        [Description("Air Cleaner Replace")]
        AirCleanerReplace = 1,
        [Description("Differential Oil Replace")]
        DifferentialOilReplace = 2,
        [Description("Emission Test")]
        EmissionTest = 3,
        [Description("Engine Oil Replace")]
        EngineOilReplace = 4,
        [Description("Fitness Report")]
        FitnessReport = 5,
        [Description("Fuel Filter Replace")]
        FuelFilterReplace = 6,
        [Description("GearBox Oil Replace")]
        GearBoxOilReplace = 7,
        [Description("Vehicle Insurenace")]
        VehicleInsurenace = 8,
        [Description("Vehicle Revenue Licence")]
        VehicleRevenueLicence = 9,
        [Description("Vehicle Repair")]
        VehicleRepair = 10,
        [Description("Tire Replace")]
        TireReplace = 11,
        [Description("Fuel Charges")]
        FuelCharges = 12,
        [Description("Other")]
        Other = 13
    }
}
