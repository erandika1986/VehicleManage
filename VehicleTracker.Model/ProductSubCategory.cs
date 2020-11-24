using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class ProductSubCategory
    {
        public ProductSubCategory()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductCategoryId { get; set; }
        public string Picture { get; set; }
        public bool? IsActive { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
