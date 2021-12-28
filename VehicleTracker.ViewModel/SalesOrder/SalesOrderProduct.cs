using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.SalesOrder
{
    public class SalesOrderProduct
    {
        public int SalesOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public decimal UntiPrice { get; set; }
        public int Qty { get; set; }
    }
}
