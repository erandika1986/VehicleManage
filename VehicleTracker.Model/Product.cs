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
            ProductInventoryOrders = new HashSet<ProductInventoryOrder>();
            ProductReturns = new HashSet<ProductReturn>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            SalesOrderItems = new HashSet<SalesOrderItem>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQty { get; set; }
        public int MinOrderQty { get; set; }
        public int MaxOrderQty { get; set; }
        public int SubProductCategoryId { get; set; }
        public int SupplierId { get; set; }
        public string Description { get; set; }
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
        public virtual ICollection<ProductInventoryOrder> ProductInventoryOrders { get; set; }
        public virtual ICollection<ProductReturn> ProductReturns { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
