using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class Product
    {
        public Product()
        {
            CustomerProductPrice = new HashSet<CustomerProductPrice>();
            ProductInventory = new HashSet<ProductInventory>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQty { get; set; }
        public int SubProductCategoryId { get; set; }
        public int SupplierId { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual ProductSubCategory SubProductCategory { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<CustomerProductPrice> CustomerProductPrice { get; set; }
        public virtual ICollection<ProductInventory> ProductInventory { get; set; }
    }
}
