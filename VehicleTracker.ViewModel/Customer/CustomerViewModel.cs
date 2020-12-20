using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Priority { get; set; }
        public long RouteId { get; set; }
        public bool IsActive { get; set; }
    }
}
