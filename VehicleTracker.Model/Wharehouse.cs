using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class Wharehouse
    {
        public Wharehouse()
        {
            PurchaseOrder = new HashSet<PurchaseOrder>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public long ManagerId { get; set; }
        public decimal? FloorSpace { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User Manager { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
