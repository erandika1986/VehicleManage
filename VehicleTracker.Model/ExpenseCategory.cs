using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class ExpenseCategory
    {
        public ExpenseCategory()
        {
            Expenses = new HashSet<Expense>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
