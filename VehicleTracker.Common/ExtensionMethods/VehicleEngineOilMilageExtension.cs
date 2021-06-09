using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleEngineOilMilageExtension
    {
        public static VehicleEngineOilMilage ToModel(this VehicleEngineOilMilageViewModel vm, VehicleEngineOilMilage model = null)
        {
            if (model == null)
                model = new VehicleEngineOilMilage();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.NextOilChangeMilage = vm.NextOilChangeMilage;
            model.OilChangeMilage = vm.OilChangeMilage;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleEngineOilMilageViewModel ToVm(this VehicleEngineOilMilage model, VehicleEngineOilMilageViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleEngineOilMilageViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.NextOilChangeMilage = model.NextOilChangeMilage;
            vm.OilChangeMilage = model.OilChangeMilage;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
