using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class Supplier
    {
        public Supplier()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
