using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Model.Enums
{
    public enum RoleType
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Driver")]
        Driver = 2,
        [Description("Manager")]
        Manager = 3,
        [Description("Sales Rep")]
        SalesRep = 4,
        [Description("Warehouse Manager")]
        WarehouseManager = 5,
        [Description("Executive")]
        Executive = 6,
        [Description("Data Entry Operator")]
        DataEntryOperator = 7
    }
}
