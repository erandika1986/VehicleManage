using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;
using System.Linq;

namespace System
{
    public static class VehicleExtension
    {
        public static Vehicle ToModel(this VehicleViewModel vm, Vehicle model = null)
        {
            if (model == null)
                model = new Vehicle();

            model.Id = vm.Id;
            model.RegistrationNo = vm.RegistrationNo;
            model.VehicelTypeId = vm.VehicelType;
            model.ProductionYear = vm.ProductionYear;
            model.InitialOdometerReading = vm.InitialOdometerReading;
            model.IsActive = vm.IsActive;
            //model.CreatedOn = vm.CreatedOn;
            //model.UpdatedOn = vm.UpdatedOn;
            //model.HasFitnessReport = vm.HasFitnessReport;
            //model.HasGreeceNipple = vm.HasGreeceNipple;
            //model.HasDifferentialOil = vm.HasDifferentialOil;

            return model;
        }

        public static VehicleViewModel ToVm(this Vehicle model, VehicleViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleViewModel();

            vm.Id = model.Id;
            vm.RegistrationNo = model.RegistrationNo;
            vm.VehicelType = model.VehicelTypeId;
            vm.ProductionYear = model.ProductionYear;
            vm.InitialOdometerReading = model.InitialOdometerReading;
            vm.VehicelTypeName = model.VehicelType.Name;
            vm.IsActive = model.IsActive;
            //vm.CreatedOn = model.CreatedOn;
            //vm.UpdatedOn = model.UpdatedOn;
            //vm.HasFitnessReport = model.HasFitnessReport;
            //vm.HasGreeceNipple = model.HasGreeceNipple;
            //vm.HasDifferentialOil = model.HasDifferentialOil;
            //vm.VehicleTypeName = model.VehicelType.Name;

            //var vehicleAirCleaner = model.VehicleAirCleaner.OrderByDescending(t => t.Id).FirstOrDefault();
            //vm.NextAirCleanerDetails = new VehicleAirCleanerViewModel()
            //{
            //    Id = vehicleAirCleaner.Id,
            //    VehicleId = vehicleAirCleaner.VehicleId,
            //    NextAirCleanerReplaceMilage = vehicleAirCleaner.NextAirCleanerReplaceMilage,
            //    ActualAirCleanerReplaceMilage = vehicleAirCleaner.ActualAirCleanerReplaceMilage,
            //    CreatedOn = vehicleAirCleaner.CreatedOn,
            //    CreatedBy = vehicleAirCleaner.CreatedBy,
            //    UpdatedBy = vehicleAirCleaner.UpdatedBy,
            //    UpdatedOn = vehicleAirCleaner.UpdatedOn,
            //    IsActive = vehicleAirCleaner.IsActive
            //};

            //var vehicleDifferentialOilDetail = model.VehicleDifferentialOilChangeMilage.OrderByDescending(t => t.Id).FirstOrDefault();
            //vm.NextDifferentialOilChangeMilageDetails = new VehicleDifferentialOilChangeMilageViewModel()
            //{
            //    Id = vehicleDifferentialOilDetail.Id,
            //    VehicleId = vehicleDifferentialOilDetail.VehicleId,
            //    NextDifferentialOilChangeMilage = vehicleDifferentialOilDetail.NextDifferentialOilChangeMilage,
            //    ActualDifferentialOilChangeMilage = vehicleDifferentialOilDetail.ActualDifferentialOilChangeMilage,
            //    CreatedOn = vehicleDifferentialOilDetail.CreatedOn,
            //    CreatedBy = vehicleDifferentialOilDetail.CreatedBy,
            //    UpdatedBy = vehicleDifferentialOilDetail.UpdatedBy,
            //    UpdatedOn = vehicleDifferentialOilDetail.UpdatedOn,
            //    IsActive = vehicleDifferentialOilDetail.IsActive
            //};

            //var vehicleEmissionTest = model.VehicleEmissiontTest.OrderByDescending(t => t.Id).FirstOrDefault();
            //vm.NextEmissionTestDetails = new VehicleEmissionTestViewModel()
            //{
            //    Id = vehicleEmissionTest.Id,
            //    VehicleId = vehicleEmissionTest.VehicleId,
            //    NextEmissiontTestDate = vehicleEmissionTest.NextEmissiontTestDate,
            //    ActualEmissiontTestDate = vehicleEmissionTest.ActualEmissiontTestDate,
            //    CreatedOn = vehicleEmissionTest.CreatedOn,
            //    CreatedBy = vehicleEmissionTest.CreatedBy,
            //    UpdatedBy = vehicleEmissionTest.UpdatedBy,
            //    UpdatedOn = vehicleEmissionTest.UpdatedOn,
            //    IsActive = vehicleEmissionTest.IsActive
            //};

            //var vehicleEngineOilMilage = model.VehicleEngineOilMilage.OrderByDescending(t => t.Id).FirstOrDefault();
            //vm.NextEngineOilMilageDetails = new VehicleEngineOilMilageViewModel()
            //{
            //    Id = vehicleEngineOilMilage.Id,
            //    VehicleId = vehicleEngineOilMilage.VehicleId,
            //    NextOilChangeMilage = vehicleEngineOilMilage.NextOilChangeMilage,
            //    ActualOilChangeMilage = vehicleEngineOilMilage.ActualOilChangeMilage,
            //    CreatedOn = vehicleEngineOilMilage.CreatedOn,
            //    CreatedBy = vehicleEngineOilMilage.CreatedBy,
            //    UpdatedBy = vehicleEngineOilMilage.UpdatedBy,
            //    UpdatedOn = vehicleEngineOilMilage.UpdatedOn,
            //    IsActive = vehicleEngineOilMilage.IsActive
            //};

            //var vehicleFitnessDetail = model.VehicleFitnessReport.OrderByDescending(t => t.Id).FirstOrDefault();
            //vm.NextFitnessReportDetails = new VehicleFitnessReportViewModel()
            //{
            //    Id = vehicleFitnessDetail.Id,
            //    VehicleId = vehicleFitnessDetail.VehicleId,
            //    NextFitnessReportDate = vehicleFitnessDetail.NextFitnessReportDate,
            //    ActualFitnessReportDate = vehicleFitnessDetail.ActualFitnessReportDate,
            //    CreatedOn = vehicleFitnessDetail.CreatedOn,
            //    CreatedBy = vehicleFitnessDetail.CreatedBy,
            //    UpdatedBy = vehicleFitnessDetail.UpdatedBy,
            //    UpdatedOn = vehicleFitnessDetail.UpdatedOn,
            //    IsActive = vehicleFitnessDetail.IsActive
            //};

            //var vehicleFuelFilterMilageDetail = model.VehicleFuelFilterMilage.OrderByDescending(t => t.Id).FirstOrDefault();
            //vm.NextFuelFilterMilageDetails = new VehicleFuelFilterMilageViewModel()
            //{
            //    Id = vehicleFuelFilterMilageDetail.Id,
            //    VehicleId = vehicleFuelFilterMilageDetail.VehicleId,
            //    NextFuelFilterChangeMilage = vehicleFuelFilterMilageDetail.NextFuelFilterChangeMilage,
            //    ActualFuelFilterChangeMilage = vehicleFuelFilterMilageDetail.ActualFuelFilterChangeMilage,
            //    CreatedOn = vehicleFuelFilterMilageDetail.CreatedOn,
            //    CreatedBy = vehicleFuelFilterMilageDetail.CreatedBy,
            //    UpdatedBy = vehicleFuelFilterMilageDetail.UpdatedBy,
            //    UpdatedOn = vehicleFuelFilterMilageDetail.UpdatedOn,
            //    IsActive = vehicleFuelFilterMilageDetail.IsActive
            //};

            //var vehicleGearBoxDetail = model.VehicleGearBoxOilMilage.OrderByDescending(t => t.Id).FirstOrDefault();
            //vm.NextGearBoxOilMilageDetails = new VehicleGearBoxOilMilageViewModel()
            //{
            //    Id = vehicleGearBoxDetail.Id,
            //    VehicleId = vehicleGearBoxDetail.VehicleId,
            //    NextGearBoxOilChangeMilage = vehicleGearBoxDetail.NextGearBoxOilChangeMilage,
            //    ActualGearBoxOilChangeMilage = vehicleGearBoxDetail.ActualGearBoxOilChangeMilage,
            //    CreatedOn = vehicleGearBoxDetail.CreatedOn,
            //    CreatedBy = vehicleGearBoxDetail.CreatedBy,
            //    UpdatedBy = vehicleGearBoxDetail.UpdatedBy,
            //    UpdatedOn = vehicleGearBoxDetail.UpdatedOn,
            //    IsActive = vehicleGearBoxDetail.IsActive
            //};




            return vm;
        }
    }
}
