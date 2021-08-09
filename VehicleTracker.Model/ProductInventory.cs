using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class ProductInventory
    {
        public ProductInventory()
        {
            ProductInventoryOrders = new HashSet<ProductInventoryOrder>();
            ProductInventoryReceivedHistories = new HashSet<ProductInventoryReceivedHistory>();
        }

        public int Id { get; set; }
        public DateTime DateRecieved { get; set; }
        public string BatchNo { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public DateTime? DateOfExpiration { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int RecievedQty { get; set; }
        public int AvailableQty { get; set; }
        public int Action { get; set; }
        public int? PurchaseOrderId { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Product Product { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual User UdatedBy { get; set; }
        public virtual Wharehouse Warehouse { get; set; }
        public virtual ICollection<ProductInventoryOrder> ProductInventoryOrders { get; set; }
        public virtual ICollection<ProductInventoryReceivedHistory> ProductInventoryReceivedHistories { get; set; }
    }
}
