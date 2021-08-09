using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class ProductInventoryReceivedHistory
    {
        public int Id { get; set; }
        public int ProductInventoryId { get; set; }
        public int ReceivedQty { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual ProductInventory ProductInventory { get; set; }
        public virtual User UpdatedBy { get; set; }
    }
}
