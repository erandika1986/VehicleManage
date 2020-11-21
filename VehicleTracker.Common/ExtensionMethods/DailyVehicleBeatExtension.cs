using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;
using VehicleTracker.ViewModel.VehicleBeat;

namespace System
{
    public static class DailyVehicleBeatExtension
    {
        public static DailyVehicleBeat ToModel(this DailyVehicleBeatViewModel vm, DailyVehicleBeat model = null)
        {
            if (model == null)
                model = new DailyVehicleBeat();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.RouteId = vm.RouteId;
            model.Date = vm.Date;
            model.Status = (int)vm.Status;
            model.StartingMilage = vm.StartingMilage;
            model.EndMilage = vm.EndMilage;
            model.CreatedOn = DateTime.UtcNow; 
            model.CreatedBy = vm.CreatedBy;
            model.UpdatedOn = DateTime.UtcNow;
            model.UpdatedBy = vm.UpdatedBy;
            model.IsActive = true;

            return model;
        }

        public static DailyVehicleBeatViewModel ToVm(this DailyVehicleBeat model, DailyVehicleBeatViewModel vm = null)
        {
            if (vm == null)
                vm = new DailyVehicleBeatViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.RouteId = model.RouteId;
            vm.Date = model.Date;
            vm.StartingMilage = model.StartingMilage.HasValue?model.StartingMilage.Value:0;
            vm.EndMilage = model.EndMilage.HasValue ? model.EndMilage.Value : 0;
            vm.Status = (DailyBeatStatus)model.Status;
            vm.EndMilage = model.EndMilage;
            vm.CreatedOn = model.CreatedOn;
            vm.CreatedBy = model.CreatedBy;
            vm.UpdatedOn = model.UpdatedOn;
            vm.UpdatedBy = model.UpdatedBy;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
