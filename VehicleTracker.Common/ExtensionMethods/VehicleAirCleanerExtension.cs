using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleAirCleanerExtension
    {
        public static VehicleAirCleaner ToModel(this VehicleAirCleanerViewModel vm, VehicleAirCleaner model = null)
        {
            if (model == null)
                model = new VehicleAirCleaner();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.NextAirCleanerReplaceMilage = vm.NextAirCleanerReplaceMilage;
            model.AirCleanerReplaceMilage = vm.AirCleanerReplaceMilage;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleAirCleanerViewModel ToVm(this VehicleAirCleaner model, VehicleAirCleanerViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleAirCleanerViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.NextAirCleanerReplaceMilage = model.NextAirCleanerReplaceMilage;
            vm.AirCleanerReplaceMilage = model.AirCleanerReplaceMilage;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
