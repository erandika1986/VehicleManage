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
            Vehicles = new List<DropDownViewModel>();
            ExpensesCategories = new List<DropDownViewModel>();
            VehicleExpenses = new List<DropDownViewModel>();
        }
        public List<DropDownViewModel> Vehicles { get; set; }
        public List<DropDownViewModel> ExpensesCategories { get; set; }
        public List<DropDownViewModel> VehicleExpenses { get; set; }
    }
}
