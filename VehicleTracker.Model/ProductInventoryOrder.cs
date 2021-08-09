using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class ProductInventoryOrder
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public long OrderId { get; set; }
        public int Qty { get; set; }

        public virtual ProductInventory Inventory { get; set; }
        public virtual Order Order { get; set; }
    }
}
