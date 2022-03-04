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
      model.Note = vm.Note;
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
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.CreatedBy = string.Format("{0} {1}", model.CreatedByNavigation.FirstName, model.CreatedByNavigation.LastName);
      vm.UpdatedBy = string.Format("{0} {1}", model.UpdatedByNavigation.FirstName, model.UpdatedByNavigation.LastName);
      vm.IsActive = model.IsActive;
      vm.Note = model.Note;
      vm.RegistrationNo = model.Vehicle.RegistrationNo;

      return vm;
    }
  }
}
