using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            DailyVehicleBeats = new HashSet<DailyVehicleBeat>();
            VehicleAirCleaners = new HashSet<VehicleAirCleaner>();
            VehicleDifferentialOilChangeMilages = new HashSet<VehicleDifferentialOilChangeMilage>();
            VehicleEmissiontTests = new HashSet<VehicleEmissiontTest>();
            VehicleEngineOilMilages = new HashSet<VehicleEngineOilMilage>();
            VehicleExpenses = new HashSet<VehicleExpense>();
            VehicleFitnessReports = new HashSet<VehicleFitnessReport>();
            VehicleFuelFilterMilages = new HashSet<VehicleFuelFilterMilage>();
            VehicleGearBoxOilMilages = new HashSet<VehicleGearBoxOilMilage>();
            VehicleGreeceNiples = new HashSet<VehicleGreeceNiple>();
            VehicleInsurances = new HashSet<VehicleInsurance>();
            VehicleRevenueLicences = new HashSet<VehicleRevenueLicence>();
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
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeats { get; set; }
        public virtual ICollection<VehicleAirCleaner> VehicleAirCleaners { get; set; }
        public virtual ICollection<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilages { get; set; }
        public virtual ICollection<VehicleEmissiontTest> VehicleEmissiontTests { get; set; }
        public virtual ICollection<VehicleEngineOilMilage> VehicleEngineOilMilages { get; set; }
        public virtual ICollection<VehicleExpense> VehicleExpenses { get; set; }
        public virtual ICollection<VehicleFitnessReport> VehicleFitnessReports { get; set; }
        public virtual ICollection<VehicleFuelFilterMilage> VehicleFuelFilterMilages { get; set; }
        public virtual ICollection<VehicleGearBoxOilMilage> VehicleGearBoxOilMilages { get; set; }
        public virtual ICollection<VehicleGreeceNiple> VehicleGreeceNiples { get; set; }
        public virtual ICollection<VehicleInsurance> VehicleInsurances { get; set; }
        public virtual ICollection<VehicleRevenueLicence> VehicleRevenueLicences { get; set; }
    }
}
