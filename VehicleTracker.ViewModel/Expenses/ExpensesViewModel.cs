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
        public ExpensesViewModel()
        {
            ExpenseImages = new List<ExpenseImageViewModel>();
        }
        public long Id { get; set; }
        public ExpenseCategoryTypes ExpenseCategoryId { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int ExpenseYear { get; set; }
        public int ExpenseMonth { get; set; }
        public int ExpenseDay { get; set; }
        public decimal Amount { get; set; }
        public long VehicleId { get; set; }
        public int VehicleExpenseTypeId { get; set; }
        public List<ExpenseImageViewModel> ExpenseImages { get; set; }

    }

    public class ExpenseImageViewModel
    {
        public string Attachment { get; set; }
        public string AttachmentName { get; set; }
    }
}
