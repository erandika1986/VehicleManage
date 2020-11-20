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
            model.NextGreeceNipleReplaceDate = vm.NextGreeceNipleReplaceDate;
            model.ActualGreeceNipleReplaceDate = vm.ActualGreeceNipleReplaceDatee;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleGreeceNipleViewModel ToVm(this VehicleGreeceNiple model, VehicleGreeceNipleViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleGreeceNipleViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.NextGreeceNipleReplaceDate = model.NextGreeceNipleReplaceDate;
            vm.ActualGreeceNipleReplaceDatee = model.ActualGreeceNipleReplaceDate;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
