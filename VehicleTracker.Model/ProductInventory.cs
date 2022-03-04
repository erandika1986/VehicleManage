using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class ProductInventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public int? ProductReturnId { get; set; }
        public int ReceivedQty { get; set; }
        public string BatchNo { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public DateTime? DateOfExpiration { get; set; }
        public int Action { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductReturn ProductReturn { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual User UdatedBy { get; set; }
        public virtual Wharehouse Warehouse { get; set; }
    }
}
