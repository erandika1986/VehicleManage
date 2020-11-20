using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Route
{
    public class RouteViewModel
    {
        public long Id { get; set; }
        public string RouteCode { get; set; }
        public string StartFrom { get; set; }
        public string EndFrom { get; set; }
        public decimal? TotalDistance { get; set; }
        public bool? IsActive { get; set; }
    }
}
