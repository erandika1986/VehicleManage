using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleEmissiontTestExtension
    {
        public static VehicleEmissiontTest ToModel(this VehicleEmissionTestViewModel vm, VehicleEmissiontTest model = null)
        {
            if (model == null)
                model = new VehicleEmissiontTest();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.ValidTill = vm.NextEmissiontTestDate;
            model.EmissiontTestDate = vm.EmissiontTestDate;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleEmissionTestViewModel ToVm(this VehicleEmissiontTest model, VehicleEmissionTestViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleEmissionTestViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.NextEmissiontTestDate = model.ValidTill;
            vm.EmissiontTestDate = model.EmissiontTestDate;
            vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
            vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
