using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class User
    {
        public User()
        {
            ClientCreatedBies = new HashSet<Client>();
            ClientUpdatedBies = new HashSet<Client>();
            CustomerProductPriceCreatedBies = new HashSet<CustomerProductPrice>();
            CustomerProductPriceUpdatedBies = new HashSet<CustomerProductPrice>();
            DailyVehicleBeatCreatedByNavigations = new HashSet<DailyVehicleBeat>();
            DailyVehicleBeatUpdatedByNavigations = new HashSet<DailyVehicleBeat>();
            OrderCreatedBies = new HashSet<Order>();
            OrderUpdatedBies = new HashSet<Order>();
            ProductCreatedBies = new HashSet<Product>();
            ProductInventoryCreatedBies = new HashSet<ProductInventory>();
            ProductInventoryUdatedBies = new HashSet<ProductInventory>();
            ProductUpdatedBies = new HashSet<Product>();
            PurchaseOrderCreatedBies = new HashSet<PurchaseOrder>();
            PurchaseOrderUpdatedBies = new HashSet<PurchaseOrder>();
            SupplierCreatedBies = new HashSet<Supplier>();
            SupplierUpdatedBies = new HashSet<Supplier>();
            UserRoles = new HashSet<UserRole>();
            VehicleAirCleanerCreatedByNavigations = new HashSet<VehicleAirCleaner>();
            VehicleAirCleanerUpdatedByNavigations = new HashSet<VehicleAirCleaner>();
            VehicleDifferentialOilChangeMilageCreatedByNavigations = new HashSet<VehicleDifferentialOilChangeMilage>();
            VehicleDifferentialOilChangeMilageUpdatedByNavigations = new HashSet<VehicleDifferentialOilChangeMilage>();
            VehicleEmissiontTestCreatedByNavigations = new HashSet<VehicleEmissiontTest>();
            VehicleEmissiontTestUpdatedByNavigations = new HashSet<VehicleEmissiontTest>();
            VehicleEngineOilMilageCreatedByNavigations = new HashSet<VehicleEngineOilMilage>();
            VehicleEngineOilMilageUpdatedByNavigations = new HashSet<VehicleEngineOilMilage>();
            VehicleExpenseCreatedByNavigations = new HashSet<VehicleExpense>();
            VehicleExpenseUpdatedByNavigations = new HashSet<VehicleExpense>();
            VehicleFitnessReportCreatedByNavigations = new HashSet<VehicleFitnessReport>();
            VehicleFitnessReportUpdatedByNavigations = new HashSet<VehicleFitnessReport>();
            VehicleFuelFilterMilageCreatedByNavigations = new HashSet<VehicleFuelFilterMilage>();
            VehicleFuelFilterMilageUpdatedByNavigations = new HashSet<VehicleFuelFilterMilage>();
            VehicleGearBoxOilMilageCreatedByNavigations = new HashSet<VehicleGearBoxOilMilage>();
            VehicleGearBoxOilMilageUpdatedByNavigations = new HashSet<VehicleGearBoxOilMilage>();
            VehicleGreeceNipleCreatedByNavigations = new HashSet<VehicleGreeceNiple>();
            VehicleGreeceNipleUpdatedByNavigations = new HashSet<VehicleGreeceNiple>();
            VehicleInsuranceCreatedByNavigations = new HashSet<VehicleInsurance>();
            VehicleInsuranceUpdatedByNavigations = new HashSet<VehicleInsurance>();
            VehicleRevenueLicenceCreatedByNavigations = new HashSet<VehicleRevenueLicence>();
            VehicleRevenueLicenceUpdatedByNavigations = new HashSet<VehicleRevenueLicence>();
            WharehouseCreatedBies = new HashSet<Wharehouse>();
            WharehouseManagers = new HashSet<Wharehouse>();
            WharehouseUpdatedBies = new HashSet<Wharehouse>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string TimeZone { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Client> ClientCreatedBies { get; set; }
        public virtual ICollection<Client> ClientUpdatedBies { get; set; }
        public virtual ICollection<CustomerProductPrice> CustomerProductPriceCreatedBies { get; set; }
        public virtual ICollection<CustomerProductPrice> CustomerProductPriceUpdatedBies { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeatCreatedByNavigations { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeatUpdatedByNavigations { get; set; }
        public virtual ICollection<Order> OrderCreatedBies { get; set; }
        public virtual ICollection<Order> OrderUpdatedBies { get; set; }
        public virtual ICollection<Product> ProductCreatedBies { get; set; }
        public virtual ICollection<ProductInventory> ProductInventoryCreatedBies { get; set; }
        public virtual ICollection<ProductInventory> ProductInventoryUdatedBies { get; set; }
        public virtual ICollection<Product> ProductUpdatedBies { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderCreatedBies { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderUpdatedBies { get; set; }
        public virtual ICollection<Supplier> SupplierCreatedBies { get; set; }
        public virtual ICollection<Supplier> SupplierUpdatedBies { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<VehicleAirCleaner> VehicleAirCleanerCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleAirCleaner> VehicleAirCleanerUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilageCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilageUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleEmissiontTest> VehicleEmissiontTestCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleEmissiontTest> VehicleEmissiontTestUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleEngineOilMilage> VehicleEngineOilMilageCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleEngineOilMilage> VehicleEngineOilMilageUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleExpense> VehicleExpenseCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleExpense> VehicleExpenseUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleFitnessReport> VehicleFitnessReportCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleFitnessReport> VehicleFitnessReportUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleFuelFilterMilage> VehicleFuelFilterMilageCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleFuelFilterMilage> VehicleFuelFilterMilageUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleGearBoxOilMilage> VehicleGearBoxOilMilageCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleGearBoxOilMilage> VehicleGearBoxOilMilageUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleGreeceNiple> VehicleGreeceNipleCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleGreeceNiple> VehicleGreeceNipleUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleInsurance> VehicleInsuranceCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleInsurance> VehicleInsuranceUpdatedByNavigations { get; set; }
        public virtual ICollection<VehicleRevenueLicence> VehicleRevenueLicenceCreatedByNavigations { get; set; }
        public virtual ICollection<VehicleRevenueLicence> VehicleRevenueLicenceUpdatedByNavigations { get; set; }
        public virtual ICollection<Wharehouse> WharehouseCreatedBies { get; set; }
        public virtual ICollection<Wharehouse> WharehouseManagers { get; set; }
        public virtual ICollection<Wharehouse> WharehouseUpdatedBies { get; set; }
    }
}
