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
            VehicleTypes = new List<DropDownViewModal>();
            ProductionYears = new List<DropDownViewModal>();
        }

        public List<DropDownViewModal> VehicleTypes { get; set; }
        public List<DropDownViewModal> ProductionYears { get; set; }
    }
}
