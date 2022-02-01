using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.VehicleBeat
{
    public class VehicleBeatFilterViewModel
    {
        public DateTime Date { get; set; }
        public int DateYear { get; set; }
        public int DateMonth { get; set; }
        public int DateDay { get; set; }
        public long SelectedRouteId { get; set; }
        public long SelectedVehicleId { get; set; }
        public long SelectedDriverId { get; set; }
        public long SelectedSalesRepId { get; set; }
        public int SelectedStatus { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }


}
