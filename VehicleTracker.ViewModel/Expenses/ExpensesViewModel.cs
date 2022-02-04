using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common.Enums;

namespace VehicleTracker.ViewModel.Expenses
{
    public class ExpensesViewModel
    {
        public long Id { get; set; }
        public ExpenseCategoryTypes ExpenseCategoryId { get; set; }
        public string Description { get; set; }
        public int ExpenseYear { get; set; }
        public int ExpenseMonth { get; set; }
        public int ExpenseDay { get; set; }
        public decimal Amount { get; set; }
        public int VehicleId { get; set; }
        public VehicleExpensesTypes VehicleExpenseTypeId { get; set; }

    }
}
