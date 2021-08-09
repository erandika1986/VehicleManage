using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class Client
    {
        public Client()
        {
            CustomerProductPrices = new HashSet<CustomerProductPrice>();
            Orders = new HashSet<Order>();
            ProductReturns = new HashSet<ProductReturn>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? Priority { get; set; }
        public long? RouteId { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedById { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Route Route { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<CustomerProductPrice> CustomerProductPrices { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductReturn> ProductReturns { get; set; }
    }
}
