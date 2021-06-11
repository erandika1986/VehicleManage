using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
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

        public virtual BreakOilCode BreakOil { get; set; }
        public virtual DifferentialOilCode DifferentialOil { get; set; }
        public virtual EgineCoolant EngineCoolant { get; set; }
        public virtual EngineOilCode EngineOil { get; set; }
        public virtual GearBoxOilCode GearBoxOil { get; set; }
        public virtual PowerSteeringOilCode PowerSteeringOil { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
