using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleGearBoxOilMilageExtension
    {
        public static VehicleGearBoxOilMilage ToModel(this VehicleGearBoxOilMilageViewModel vm, VehicleGearBoxOilMilage model = null)
        {
            if (model == null)
                model = new VehicleGearBoxOilMilage();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.NextGearBoxOilChangeMilage = vm.NextGearBoxOilChangeMilage;
            model.GearBoxOilChangeMilage = vm.GearBoxOilChangeMilage;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleGearBoxOilMilageViewModel ToVm(this VehicleGearBoxOilMilage model, VehicleGearBoxOilMilageViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleGearBoxOilMilageViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.NextGearBoxOilChangeMilage = model.NextGearBoxOilChangeMilage;
            vm.GearBoxOilChangeMilage = model.GearBoxOilChangeMilage;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
