using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class Supplier
    {
        public Supplier()
        {
            Product = new HashSet<Product>();
            PurchaseOrder = new HashSet<PurchaseOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Bank { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public string BranchCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }
        public bool? IsActive { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
