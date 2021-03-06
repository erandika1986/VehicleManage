﻿using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.VehicleBeat
{
    public class VehicleBeatMasterDataViewModel
    {
        public VehicleBeatMasterDataViewModel()
        {
            Vehicles = new List<DropDownViewModal>();
            Routes = new List<DropDownViewModal>();
            Status = new List<DropDownViewModal>();
        }

        public List<DropDownViewModal> Vehicles { get; set; }
        public List<DropDownViewModal> Routes { get; set; }
        public List<DropDownViewModal> Status { get; set; }
    }
}
