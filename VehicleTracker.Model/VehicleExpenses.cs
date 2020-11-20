using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleExpenses
    {
        public long Id { get; set; }
        public int ExpenseType { get; set; }
        public long VehicleId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
