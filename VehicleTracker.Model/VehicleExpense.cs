using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class VehicleExpense
    {
        public long Id { get; set; }
        public int VehicleExpenseType { get; set; }
        public long VehicleId { get; set; }

        public virtual Expense IdNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
