using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class SalesOrderStatus
    {
        public SalesOrderStatus()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public int? RemindDays { get; set; }
        public int Sequence { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
