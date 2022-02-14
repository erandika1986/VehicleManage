using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.Expenses
{
    public class ExpensesMasterDataViewModel
    {
        public ExpensesMasterDataViewModel()
        {
            Vehicles = new List<DropDownViewModal>();
            ExpensesCategories = new List<DropDownViewModal>();
            VehicleExpenses = new List<DropDownViewModal>();
        }
        public List<DropDownViewModal> Vehicles { get; set; }
        public List<DropDownViewModal> ExpensesCategories { get; set; }
        public List<DropDownViewModal> VehicleExpenses { get; set; }
    }
}
