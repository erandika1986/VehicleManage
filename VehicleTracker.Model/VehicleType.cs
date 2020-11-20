using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int? EngineOilId { get; set; }
        public int? EngineOilChangeMilage { get; set; }
        public int? FuelFilterChangeMilage { get; set; }
        public int? GearBoxOilId { get; set; }
        public int? GearBoxChangeMilage { get; set; }
        public bool HasDifferentialOil { get; set; }
        public int? DifferentialOilChangeMilage { get; set; }
        public int? DifferentialOilId { get; set; }
        public string FuelFilterNumber { get; set; }
        public int? AirCleanerMilage { get; set; }
        public bool HasGreeceNipple { get; set; }
        public int? GreeceNipleAge { get; set; }
        public int InsuranceAge { get; set; }
        public bool HasFitnessReport { get; set; }
        public int? FitnessReportAge { get; set; }
        public int EmitionTestAge { get; set; }
        public int RevenueLicenceAge { get; set; }
        public int FuelType { get; set; }
        public int? BreakOilId { get; set; }
        public int? EngineCoolantId { get; set; }
        public int? PowerSteeringOilId { get; set; }

        public virtual BreakOilCodes BreakOil { get; set; }
        public virtual DifferentialOilCodes DifferentialOil { get; set; }
        public virtual EgineCoolants EngineCoolant { get; set; }
        public virtual EngineOilCodes EngineOil { get; set; }
        public virtual GearBoxOilCodes GearBoxOil { get; set; }
        public virtual PowerSteeringOilCodes PowerSteeringOil { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}
