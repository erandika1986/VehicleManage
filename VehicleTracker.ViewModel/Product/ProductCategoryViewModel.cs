using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
    }
}
