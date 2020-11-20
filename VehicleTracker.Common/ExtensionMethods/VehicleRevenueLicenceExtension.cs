using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleRevenueLicenceExtension
    {
        public static VehicleRevenueLicence ToModel(this VehicleRevenueLicenceViewModel vm, VehicleRevenueLicence model = null)
        {
            if (model == null)
                model = new VehicleRevenueLicence();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.NextRevenueLicenceDate = vm.NextRevenueLicenceDate;
            model.ActualRevenueLicenceDate = vm.ActualRevenueLicenceDate;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleRevenueLicenceViewModel ToVm(this VehicleRevenueLicence model, VehicleRevenueLicenceViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleRevenueLicenceViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.NextRevenueLicenceDate = model.NextRevenueLicenceDate;
            vm.ActualRevenueLicenceDate = model.ActualRevenueLicenceDate;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
