using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common.Enums;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleExpenseViewModel
    {
        public long Id { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public long VehicleId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
    }
}
