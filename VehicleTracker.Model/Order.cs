using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class Order
    {
        public Order()
        {
            DailyVehicleBeatOrders = new HashSet<DailyVehicleBeatOrder>();
            OrderItems = new HashSet<OrderItem>();
            ProductInventoryOrders = new HashSet<ProductInventoryOrder>();
            ProductReturns = new HashSet<ProductReturn>();
        }

        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public int OwnerId { get; set; }
        public int Status { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Client Owner { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<DailyVehicleBeatOrder> DailyVehicleBeatOrders { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductInventoryOrder> ProductInventoryOrders { get; set; }
        public virtual ICollection<ProductReturn> ProductReturns { get; set; }
    }
}
