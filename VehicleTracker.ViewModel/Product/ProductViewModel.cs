using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int ProductSubCategoryId { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQty { get; set; }
        public int SupplierId { get; set; }

        public string Description { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
    }
}
