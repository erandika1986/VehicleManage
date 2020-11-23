using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.VehicleBeat
{
    public class VehicleBeatFilterViewModel
    {
        public string Date { get; set; }
        //public long SelectedRouteId { get; set; }
        //public long SelectedVehicleId { get; set; }
        //public DateTime FromDate { get; set; }
        //public DateTime ToDate { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }


}
