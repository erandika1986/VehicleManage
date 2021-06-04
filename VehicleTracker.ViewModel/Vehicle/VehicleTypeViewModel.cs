using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleTypeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? EngineOilChangeMilage { get; set; }
        public int EngineOilId { get; set; }
        public int? FuelFilterChangeMilage { get; set; }
        public int? GearBoxChangeMilage { get; set; }
        public int GearBoxOilId { get; set; }
        public bool HasDifferentialOil { get; set; }
        public int? DifferentialOilChangeMilage { get; set; }
        public int DifferentialOilId { get; set; }
        public string FuelFilterNumber { get; set; }
        public int? AirCleanerAge { get; set; }
        public bool HasGreeceNipple { get; set; }
        public int? GreeceNipleAge { get; set; }
        public int InsuranceAge { get; set; }
        public bool HasFitnessReport { get; set; }
        public int? FitnessReportAge { get; set; }
        public int EmitionTestAge { get; set; }
        public int RevenueLicenceAge { get; set; }
        public int FuelType { get; set; }
        //public int BreakOilMilage { get; set; }
        public int BreakOilId { get; set; }
        //public int EngineCoolantMilage { get; set; }
        public int EngineCoolantId { get; set; }
        //public int PowerSteeringOilMilage { get; set; }
        public int PowerSteeringOilId { get; set; }


        //For List View
        public string FuelTypeName { get; set; }
        public string GearBoxOilNumber { get; set; }
        public string DifferentialOilNumber { get; set; }
        public string EngineOilNumber { get; set; }

        public string ImageUrl { get; set; }
  }
}
