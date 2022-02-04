using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VehicleTracker.ViewModel.Common.Enums
{
    public enum ExpenseCategoryTypes
    {
        [Description("Electricity")]
        Electricity = 1,

        [Description("Vehicle Expenses")]
        VehicleExpenses = 5,
    }
}
