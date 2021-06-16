using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
  public static class VehicleDifferentialOilChangeMilageExtension
  {
    public static VehicleDifferentialOilChangeMilage ToModel(this VehicleDifferentialOilChangeMilageViewModel vm, VehicleDifferentialOilChangeMilage model = null)
    {
      if (model == null)
        model = new VehicleDifferentialOilChangeMilage();

      model.Id = vm.Id;
      model.VehicleId = vm.VehicleId;
      model.NextDifferentialOilChangeMilage = vm.NextDifferentialOilChangeMilage;
      model.DifferentialOilChangeMilage = vm.DifferentialOilChangeMilage;
      model.CreatedOn = DateTime.UtcNow;
      model.UpdatedOn = DateTime.UtcNow;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static VehicleDifferentialOilChangeMilageViewModel ToVm(this VehicleDifferentialOilChangeMilage model, VehicleDifferentialOilChangeMilageViewModel vm = null)
    {
      if (vm == null)
        vm = new VehicleDifferentialOilChangeMilageViewModel();

      vm.Id = model.Id;
      vm.VehicleId = model.VehicleId;
      vm.NextDifferentialOilChangeMilage = model.NextDifferentialOilChangeMilage;
      vm.DifferentialOilChangeMilage = model.DifferentialOilChangeMilage;
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.CreatedBy = string.Format("{0} {1}", model.CreatedByNavigation.FirstName, model.CreatedByNavigation.LastName);
      vm.UpdatedBy = string.Format("{0} {1}", model.UpdatedByNavigation.FirstName, model.UpdatedByNavigation.LastName);
      vm.IsActive = model.IsActive;
      vm.RegistrationNo = model.Vehicle.RegistrationNo;

      return vm;
    }
  }
}
