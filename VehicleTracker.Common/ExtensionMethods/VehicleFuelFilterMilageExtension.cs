﻿using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleFuelFilterMilageExtension
    {
        public static VehicleFuelFilterMilage ToModel(this VehicleFuelFilterMilageViewModel vm, VehicleFuelFilterMilage model = null)
        {
            if (model == null)
                model = new VehicleFuelFilterMilage();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.NextFuelFilterChangeMilage = vm.NextFuelFilterChangeMilage;
            model.ActualFuelFilterChangeMilage = vm.ActualFuelFilterChangeMilage;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleFuelFilterMilageViewModel ToVm(this VehicleFuelFilterMilage model, VehicleFuelFilterMilageViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleFuelFilterMilageViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.NextFuelFilterChangeMilage = model.NextFuelFilterChangeMilage;
            vm.ActualFuelFilterChangeMilage = model.ActualFuelFilterChangeMilage;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
