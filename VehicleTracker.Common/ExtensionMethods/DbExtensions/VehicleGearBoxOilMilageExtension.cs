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
      model.Note = vm.Note;
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
