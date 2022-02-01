using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.SalesOrder
{
    public class SalesOrderItemViewModel
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public int SelectedCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int SelectedSubCategoryId { get; set; }
        public long SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
