using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductSubCategory = new HashSet<ProductSubCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ProductSubCategory> ProductSubCategory { get; set; }
    }
}
