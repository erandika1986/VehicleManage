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
            model.NextEmissiontTestDate = vm.NextEmissiontTestDate;
            model.ActualEmissiontTestDate = vm.ActualEmissiontTestDate;
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
            vm.NextEmissiontTestDate = model.NextEmissiontTestDate;
            vm.ActualEmissiontTestDate = model.ActualEmissiontTestDate;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
