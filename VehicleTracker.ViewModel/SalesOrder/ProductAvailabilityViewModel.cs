using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.SalesOrder
{
    public class ProductAvailabilityViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int WarehouseId { get; set; }
        public string WarhouseName { get; set; }
        public int AvailableQty { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderedQty { get; set; }
    }
}
