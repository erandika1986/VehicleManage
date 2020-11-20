using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            DailyVehicleBeat = new HashSet<DailyVehicleBeat>();
            VehicleAirCleaner = new HashSet<VehicleAirCleaner>();
            VehicleDifferentialOilChangeMilage = new HashSet<VehicleDifferentialOilChangeMilage>();
            VehicleEmissiontTest = new HashSet<VehicleEmissiontTest>();
            VehicleEngineOilMilage = new HashSet<VehicleEngineOilMilage>();
            VehicleExpenses = new HashSet<VehicleExpenses>();
            VehicleFitnessReport = new HashSet<VehicleFitnessReport>();
            VehicleFuelFilterMilage = new HashSet<VehicleFuelFilterMilage>();
            VehicleGearBoxOilMilage = new HashSet<VehicleGearBoxOilMilage>();
            VehicleGreeceNiple = new HashSet<VehicleGreeceNiple>();
            VehicleInsurance = new HashSet<VehicleInsurance>();
            VehicleRevenueLicence = new HashSet<VehicleRevenueLicence>();
        }

        public long Id { get; set; }
        public string RegistrationNo { get; set; }
        public long VehicelTypeId { get; set; }
        public int ProductionYear { get; set; }
        public decimal InitialOdometerReading { get; set; }
        public bool HasFitnessReport { get; set; }
        public bool HasGreeceNipple { get; set; }
        public bool HasDifferentialOil { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual VehicleType VehicelType { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeat { get; set; }
        public virtual ICollection<VehicleAirCleaner> VehicleAirCleaner { get; set; }
        public virtual ICollection<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilage { get; set; }
        public virtual ICollection<VehicleEmissiontTest> VehicleEmissiontTest { get; set; }
        public virtual ICollection<VehicleEngineOilMilage> VehicleEngineOilMilage { get; set; }
        public virtual ICollection<VehicleExpenses> VehicleExpenses { get; set; }
        public virtual ICollection<VehicleFitnessReport> VehicleFitnessReport { get; set; }
        public virtual ICollection<VehicleFuelFilterMilage> VehicleFuelFilterMilage { get; set; }
        public virtual ICollection<VehicleGearBoxOilMilage> VehicleGearBoxOilMilage { get; set; }
        public virtual ICollection<VehicleGreeceNiple> VehicleGreeceNiple { get; set; }
        public virtual ICollection<VehicleInsurance> VehicleInsurance { get; set; }
        public virtual ICollection<VehicleRevenueLicence> VehicleRevenueLicence { get; set; }
    }
}
