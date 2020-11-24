using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class Order
    {
        public Order()
        {
            DailyVehicleBeatOrders = new HashSet<DailyVehicleBeatOrders>();
            OrderItems = new HashSet<OrderItems>();
        }

        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
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
        public virtual ICollection<DailyVehicleBeatOrders> DailyVehicleBeatOrders { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
