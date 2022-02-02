using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.Expenses
{
    public class ExpensesViewModel
    {
        public long Id { get; set; }
        public int ExpenseCategory { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int ExpenseYear { get; set; }
        public int ExpenseMonth { get; set; }
        public int ExpenseDay { get; set; }
        public decimal Amount { get; set; }
        public int VehicleId { get; set; }
        public int VehicleCategoryTypeId { get; set; }

    }
}
