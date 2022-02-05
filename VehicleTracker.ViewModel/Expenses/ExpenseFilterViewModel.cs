using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common.Enums;

namespace VehicleTracker.ViewModel.Expenses
{
    public class ExpenseFilterViewModel
    {
        public int ExpenseCategoryId { get; set; }
        public DateTime FromDate { get; set; }
        public int FromYear { get; set; }
        public int FromMonth { get; set; }
        public int FromDay { get; set; }
        public DateTime ToDate { get; set; }
        public int ToYear { get; set; }
        public int ToMonth { get; set; }
        public int ToDay { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
