using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleInsuranceExtension
    {
        public static VehicleInsurance ToModel(this VehicleInsuranceViewModel vm, VehicleInsurance model = null)
        {
            if (model == null)
                model = new VehicleInsurance();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.NextInsuranceDate = vm.NextInsuranceDate;
            model.ActualInsuranceDate = vm.ActualInsuranceDate;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleInsuranceViewModel ToVm(this VehicleInsurance model, VehicleInsuranceViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleInsuranceViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
      
            vm.NextInsuranceDate = model.NextInsuranceDate;
            vm.ActualInsuranceDate = model.ActualInsuranceDate;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
