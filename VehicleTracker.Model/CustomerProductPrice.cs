using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class CustomerProductPrice
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal AssignedUnitPrice { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Client Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual User UpdatedBy { get; set; }
    }
}
