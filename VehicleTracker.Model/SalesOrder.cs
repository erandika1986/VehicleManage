using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            DailyVehicleBeatOrders = new HashSet<DailyVehicleBeatOrder>();
            ProductInventoryOrders = new HashSet<ProductInventoryOrder>();
            ProductReturns = new HashSet<ProductReturn>();
            SalesOrderItems = new HashSet<SalesOrderItem>();
        }

        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public int? OwnerId { get; set; }
        public int Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal ShippingCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Client Owner { get; set; }
        public virtual SalesOrderStatus StatusNavigation { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<DailyVehicleBeatOrder> DailyVehicleBeatOrders { get; set; }
        public virtual ICollection<ProductInventoryOrder> ProductInventoryOrders { get; set; }
        public virtual ICollection<ProductReturn> ProductReturns { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
}
