using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.VehicleBeat
{
    public class VehicleBeatMasterDataViewModel
    {
        public VehicleBeatMasterDataViewModel()
        {
            Vehicles = new List<DropDownViewModel>();
            Routes = new List<DropDownViewModel>();
            Status = new List<DropDownViewModel>();
            Drivers = new List<DropDownViewModel>();
            SalesReps = new List<DropDownViewModel>();
        }

        public List<DropDownViewModel> Vehicles { get; set; }
        public List<DropDownViewModel> Routes { get; set; }
        public List<DropDownViewModel> Status { get; set; }
        public List<DropDownViewModel> Drivers { get; set; }
        public List<DropDownViewModel> SalesReps { get; set; }
    }
}
