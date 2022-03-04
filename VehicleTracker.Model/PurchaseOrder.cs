using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            ProductInventories = new HashSet<ProductInventory>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
            PurchaseOrderSendingHistories = new HashSet<PurchaseOrderSendingHistory>();
        }

        public int Id { get; set; }
        public string Ponumber { get; set; }
        public int SupplierId { get; set; }
        public int ShippedToWharehouseId { get; set; }
        public DateTime Date { get; set; }
        public string Terms { get; set; }
        public string Remark { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal ShipingCharge { get; set; }
        public decimal Total { get; set; }
        public int Status { get; set; }
        public string Attachment { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Wharehouse ShippedToWharehouse { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<PurchaseOrderSendingHistory> PurchaseOrderSendingHistories { get; set; }
    }
}
