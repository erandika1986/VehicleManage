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
      model.ValidTill = new DateTime(vm.ValidTillYear, vm.ValidTillMonth, vm.ValidTillDay);
      model.InsuranceDate = new DateTime(vm.InsuranceYear, vm.InsuranceMonth, vm.InsuranceDay);

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

      vm.ValidTill = model.ValidTill.ToString("MMMM dd, yyyy");
      vm.InsuranceDate = model.InsuranceDate.ToString("MMMM dd, yyyy");
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.IsActive = model.IsActive;
      vm.ValidTillYear = model.ValidTill.Year;
      vm.ValidTillMonth = model.ValidTill.Month;
      vm.ValidTillDay = model.ValidTill.Day;
      vm.RegistrationNo = model.Vehicle.RegistrationNo;

      vm.InsuranceYear = model.InsuranceDate.Year;
      vm.InsuranceMonth = model.InsuranceDate.Month;
      vm.InsuranceDay = model.InsuranceDate.Day;
      return vm;

    }
  }
}
