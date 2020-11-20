using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleFilterViewModel
    {
        public bool Status { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
