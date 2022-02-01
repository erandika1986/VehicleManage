using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class Expense
    {
        public Expense()
        {
            ExpenseImages = new HashSet<ExpenseImage>();
        }

        public long Id { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual VehicleExpense VehicleExpense { get; set; }
        public virtual ICollection<ExpenseImage> ExpenseImages { get; set; }
    }
}
