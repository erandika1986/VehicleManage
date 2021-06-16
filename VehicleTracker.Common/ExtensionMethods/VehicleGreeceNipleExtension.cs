using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
  public static class VehicleGreeceNipleExtension
  {
    public static VehicleGreeceNiple ToModel(this VehicleGreeceNipleViewModel vm, VehicleGreeceNiple model = null)
    {
      if (model == null)
        model = new VehicleGreeceNiple();

      model.Id = vm.Id;
      model.VehicleId = vm.VehicleId;
      model.NextGreeceNipleReplaceDate = new DateTime(vm.GreeceNipleReplacYear, vm.GreeceNipleReplacMonth, vm.GreeceNipleReplacDay, 0, 0, 0);
      model.GreeceNipleReplaceDate = new DateTime(vm.NextGreeceNipleReplaceYear, vm.NextGreeceNipleReplaceMonth, vm.NextGreeceNipleReplaceDay, 0, 0, 0);
      model.CreatedOn = DateTime.UtcNow;
      model.UpdatedOn = DateTime.UtcNow;
      model.Note = vm.Note;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static VehicleGreeceNipleViewModel ToVm(this VehicleGreeceNiple model, VehicleGreeceNipleViewModel vm = null)
    {
      if (vm == null)
        vm = new VehicleGreeceNipleViewModel();

      vm.Id = model.Id;
      vm.VehicleId = model.VehicleId;
      vm.NextGreeceNipleReplaceDate = model.NextGreeceNipleReplaceDate.ToString("MMMM dd, yyyy");
      vm.GreeceNipleReplaceDate = model.GreeceNipleReplaceDate.ToString("MMMM dd, yyyy");
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.NextGreeceNipleReplaceYear = model.GreeceNipleReplaceDate.Year;
      vm.NextGreeceNipleReplaceMonth = model.GreeceNipleReplaceDate.Month;
      vm.NextGreeceNipleReplaceDay = model.GreeceNipleReplaceDate.Day;
      vm.GreeceNipleReplacYear = model.NextGreeceNipleReplaceDate.Year;
      vm.GreeceNipleReplacMonth = model.NextGreeceNipleReplaceDate.Month;
      vm.GreeceNipleReplacDay = model.NextGreeceNipleReplaceDate.Day;
      vm.CreatedBy = string.Format("{0} {1}", model.CreatedByNavigation.FirstName, model.CreatedByNavigation.LastName);
      vm.UpdatedBy = string.Format("{0} {1}", model.UpdatedByNavigation.FirstName, model.UpdatedByNavigation.LastName);
      vm.IsActive = model.IsActive;
      vm.Note = model.Note;
      vm.RegistrationNo = model.Vehicle.RegistrationNo;

      return vm;
    }
  }
}
