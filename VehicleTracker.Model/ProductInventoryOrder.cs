using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class ProductInventoryOrder
    {
        public int Id { get; set; }
        public long? OrderId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Qty { get; set; }
        public int ActionType { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual SalesOrder Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual Wharehouse Warehouse { get; set; }
    }
}
