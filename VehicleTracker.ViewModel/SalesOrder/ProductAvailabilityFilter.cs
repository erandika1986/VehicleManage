using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.SalesOrder
{
    public class ProductAvailabilityFilter
    {
        public int ProductCategoryId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
