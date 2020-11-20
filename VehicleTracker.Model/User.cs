using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class User
    {
        public User()
        {
            DailyVehicleBeatCreatedByNavigation = new HashSet<DailyVehicleBeat>();
            DailyVehicleBeatUpdatedByNavigation = new HashSet<DailyVehicleBeat>();
            UserRole = new HashSet<UserRole>();
            VehicleAirCleanerCreatedByNavigation = new HashSet<VehicleAirCleaner>();
            VehicleAirCleanerUpdatedByNavigation = new HashSet<VehicleAirCleaner>();
            VehicleDifferentialOilChangeMilage = new HashSet<VehicleDifferentialOilChangeMilage>();
            VehicleEmissiontTest = new HashSet<VehicleEmissiontTest>();
            VehicleEngineOilMilageCreatedByNavigation = new HashSet<VehicleEngineOilMilage>();
            VehicleEngineOilMilageUpdatedByNavigation = new HashSet<VehicleEngineOilMilage>();
            VehicleExpensesCreatedByNavigation = new HashSet<VehicleExpenses>();
            VehicleExpensesUpdatedByNavigation = new HashSet<VehicleExpenses>();
            VehicleFuelFilterMilageCreatedByNavigation = new HashSet<VehicleFuelFilterMilage>();
            VehicleFuelFilterMilageUpdatedByNavigation = new HashSet<VehicleFuelFilterMilage>();
            VehicleGearBoxOilMilageCreatedByNavigation = new HashSet<VehicleGearBoxOilMilage>();
            VehicleGearBoxOilMilageUpdatedByNavigation = new HashSet<VehicleGearBoxOilMilage>();
            VehicleGreeceNipleCreatedByNavigation = new HashSet<VehicleGreeceNiple>();
            VehicleGreeceNipleUpdatedByNavigation = new HashSet<VehicleGreeceNiple>();
            VehicleInsuranceCreatedByNavigation = new HashSet<VehicleInsurance>();
            VehicleInsuranceUpdatedByNavigation = new HashSet<VehicleInsurance>();
            VehicleRevenueLicenceCreatedByNavigation = new HashSet<VehicleRevenueLicence>();
            VehicleRevenueLicenceUpdatedByNavigation = new HashSet<VehicleRevenueLicence>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeatCreatedByNavigation { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeatUpdatedByNavigation { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<VehicleAirCleaner> VehicleAirCleanerCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleAirCleaner> VehicleAirCleanerUpdatedByNavigation { get; set; }
        public virtual ICollection<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilage { get; set; }
        public virtual ICollection<VehicleEmissiontTest> VehicleEmissiontTest { get; set; }
        public virtual ICollection<VehicleEngineOilMilage> VehicleEngineOilMilageCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleEngineOilMilage> VehicleEngineOilMilageUpdatedByNavigation { get; set; }
        public virtual ICollection<VehicleExpenses> VehicleExpensesCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleExpenses> VehicleExpensesUpdatedByNavigation { get; set; }
        public virtual ICollection<VehicleFuelFilterMilage> VehicleFuelFilterMilageCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleFuelFilterMilage> VehicleFuelFilterMilageUpdatedByNavigation { get; set; }
        public virtual ICollection<VehicleGearBoxOilMilage> VehicleGearBoxOilMilageCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleGearBoxOilMilage> VehicleGearBoxOilMilageUpdatedByNavigation { get; set; }
        public virtual ICollection<VehicleGreeceNiple> VehicleGreeceNipleCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleGreeceNiple> VehicleGreeceNipleUpdatedByNavigation { get; set; }
        public virtual ICollection<VehicleInsurance> VehicleInsuranceCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleInsurance> VehicleInsuranceUpdatedByNavigation { get; set; }
        public virtual ICollection<VehicleRevenueLicence> VehicleRevenueLicenceCreatedByNavigation { get; set; }
        public virtual ICollection<VehicleRevenueLicence> VehicleRevenueLicenceUpdatedByNavigation { get; set; }
    }
}
