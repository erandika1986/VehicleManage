using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common.Enums;

namespace VehicleTracker.ViewModel.Expenses
{
    public class VehicleExpensesViewModel
    {
        public long Id { get; set; }
        public VehicleExpensesTypes VehicleExpensesType { get; set; }
        public int VehicleId { get; set; }
    }

}
