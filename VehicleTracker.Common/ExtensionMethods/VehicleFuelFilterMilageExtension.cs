using System;
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
      model.FuelFilterChangeMilage = vm.FuelFilterChangeMilage;
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
      vm.FuelFilterChangeMilage = model.FuelFilterChangeMilage;
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.IsActive = model.IsActive.Value;

      return vm;
    }
  }
}
