using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class Product
    {
        public Product()
        {
            CustomerProductPrices = new HashSet<CustomerProductPrice>();
            ProductImages = new HashSet<ProductImage>();
            ProductInventories = new HashSet<ProductInventory>();
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
        public virtual ICollection<CustomerProductPrice> CustomerProductPrices { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
    }
}
