using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleFitnessReportExtension
    {
        public static VehicleFitnessReport ToModel(this VehicleFitnessReportViewModel vm, VehicleFitnessReport model = null)
        {
            if (model == null)
                model = new VehicleFitnessReport();

            model.Id = vm.Id;
            model.VehicleId = vm.VehicleId;
            model.ValidTill = vm.ValidTill;
            model.FitnessReportDate = vm.FitnessReportDate;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static VehicleFitnessReportViewModel ToVm(this VehicleFitnessReport model, VehicleFitnessReportViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleFitnessReportViewModel();

            vm.Id = model.Id;
            vm.VehicleId = model.VehicleId;
            vm.ValidTill = model.ValidTill;
            vm.FitnessReportDate = model.FitnessReportDate;
            vm.CreatedOn = model.CreatedOn;
            vm.UpdatedOn = model.UpdatedOn;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
