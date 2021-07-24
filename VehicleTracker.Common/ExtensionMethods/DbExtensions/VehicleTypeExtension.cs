using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common.Enums;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleTypeExtension
    {
        public static VehicleType ToModel(this VehicleTypeViewModel vm,VehicleType model=null)
        {
            if (model == null)
                model = new VehicleType();

            model.Name = vm.Name;
            model.EngineOilChangeMilage = vm.EngineOilChangeMilage;
            model.FuelFilterChangeMilage = vm.FuelFilterChangeMilage;
            model.GearBoxChangeMilage = vm.GearBoxChangeMilage;
            model.HasDifferentialOil = vm.HasDifferentialOil;
            if(vm.HasDifferentialOil)
            {
                model.DifferentialOilChangeMilage = vm.DifferentialOilChangeMilage;
                model.DifferentialOilId = vm.DifferentialOilId > 0 ? vm.DifferentialOilId : (int?)null;
            }
            else
            {
                model.DifferentialOilChangeMilage = (int?)null;
                model.DifferentialOilId = (int?)null;
            }

            model.FuelFilterNumber = vm.FuelFilterNumber;
            model.AirCleanerMilage = vm.AirCleanerAge;
            if(vm.HasGreeceNipple)
            {
                model.GreeceNipleAge = vm.GreeceNipleAge;
            }
            else
            {
                model.GreeceNipleAge = (int?)null;
            }

            model.InsuranceAge = vm.InsuranceAge;
            model.HasFitnessReport = vm.HasFitnessReport;
            if(vm.HasFitnessReport)
            {
                model.FitnessReportAge = vm.FitnessReportAge;
            }
            else
            {
                model.FitnessReportAge = (int?)null;
            }

            model.EmitionTestAge = vm.EmitionTestAge;
            model.RevenueLicenceAge = vm.RevenueLicenceAge;
            model.FuelType = vm.FuelType;
            model.BreakOilId = vm.EngineOilId > 0 ? vm.BreakOilId : (int?)null;

            model.EngineCoolantId = vm.EngineCoolantId > 0 ? vm.EngineCoolantId : (int?)null;
            model.EngineOilId = vm.EngineOilId > 0 ? vm.EngineOilId : (int?)null;
            model.GearBoxOilId = vm.GearBoxOilId > 0 ? vm.GearBoxOilId : (int?)null;
            model.PowerSteeringOilId = vm.PowerSteeringOilId > 0 ? vm.PowerSteeringOilId : (int?)null;

            return model;
        }

        public static VehicleTypeViewModel ToVm(this VehicleType model, VehicleTypeViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleTypeViewModel();

            vm.Id = model.Id;
            vm.Name = model.Name;
            vm.EngineOilChangeMilage = model.EngineOilChangeMilage;
            vm.FuelFilterChangeMilage = model.FuelFilterChangeMilage;
            vm.GearBoxChangeMilage = model.GearBoxChangeMilage;
            vm.HasDifferentialOil = model.HasDifferentialOil;
            vm.DifferentialOilChangeMilage = model.DifferentialOilChangeMilage;
            vm.DifferentialOilId = model.DifferentialOilId.HasValue ? model.DifferentialOilId.Value : 0;
            vm.FuelFilterNumber = model.FuelFilterNumber;
            vm.AirCleanerAge = model.AirCleanerMilage;
            vm.HasGreeceNipple = model.HasGreeceNipple;
            vm.GreeceNipleAge = model.GreeceNipleAge;
            vm.InsuranceAge = model.InsuranceAge;
            vm.HasFitnessReport = model.HasFitnessReport;
            vm.FitnessReportAge = model.FitnessReportAge;
            vm.EmitionTestAge = model.EmitionTestAge;
            vm.RevenueLicenceAge = model.RevenueLicenceAge;
            vm.FuelType = model.FuelType;
            vm.FuelTypeName = model.FuelType != 0 ? EnumHelper.GetEnumDescription((FuelType)model.FuelType) : string.Empty;
            vm.BreakOilId = model.BreakOilId.HasValue ? model.BreakOilId.Value : 0;

            vm.EngineCoolantId = model.EngineCoolantId.HasValue ? model.EngineCoolantId.Value : 0;
            vm.EngineOilId = model.EngineOilId.HasValue ? model.EngineOilId.Value : 0;
            vm.GearBoxOilId = model.GearBoxOilId.HasValue ? model.GearBoxOilId.Value : 0;
            vm.PowerSteeringOilId = model.PowerSteeringOilId.HasValue ? model.PowerSteeringOilId.Value : 0;

            //For Table
            vm.GearBoxOilNumber = model.GearBoxOilId.HasValue ? model.GearBoxOil.Code : string.Empty;
            vm.DifferentialOilNumber = model.DifferentialOilId.HasValue ? model.DifferentialOil.Code : string.Empty;
            vm.EngineOilNumber = model.EngineOilId.HasValue ? model.EngineOil.Code : string.Empty;

            return vm;
        }
    }
}
