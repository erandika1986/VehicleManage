using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleMasterDataViewModel
    {
        public VehicleMasterDataViewModel()
        {
            VehicleTypes = new List<DropDownViewModel>();
            ProductionYears = new List<DropDownViewModel>();
        }

        public List<DropDownViewModel> VehicleTypes { get; set; }
        public List<DropDownViewModel> ProductionYears { get; set; }
    }
}
