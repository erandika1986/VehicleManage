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
            DailyVehicleBeatDrivers = new HashSet<DailyVehicleBeat>();
            DailyVehicleBeatOrders = new HashSet<DailyVehicleBeatOrder>();
            DailyVehicleBeatSalesReps = new HashSet<DailyVehicleBeat>();
            DailyVehicleBeatUpdatedByNavigations = new HashSet<DailyVehicleBeat>();
            ExpenseCreatedBies = new HashSet<Expense>();
            ExpenseUpdatedBies = new HashSet<Expense>();
            ProductCreatedBies = new HashSet<Product>();
            ProductInventoryCreatedBies = new HashSet<ProductInventory>();
            ProductInventoryOrderCreatedBies = new HashSet<ProductInventoryOrder>();
            ProductInventoryOrderUpdatedBies = new HashSet<ProductInventoryOrder>();
            ProductInventoryUdatedBies = new HashSet<ProductInventory>();
            ProductReturnCreatedBies = new HashSet<ProductReturn>();
            ProductReturnUpdatedBies = new HashSet<ProductReturn>();
            ProductUpdatedBies = new HashSet<Product>();
            PurchaseOrderCreatedBies = new HashSet<PurchaseOrder>();
            PurchaseOrderSendingHistories = new HashSet<PurchaseOrderSendingHistory>();
            PurchaseOrderUpdatedBies = new HashSet<PurchaseOrder>();
            SalesOrderCreatedBies = new HashSet<SalesOrder>();
            SalesOrderUpdatedBies = new HashSet<SalesOrder>();
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
        public string Image { get; set; }
        public int? TimeZoneId { get; set; }
        public string Nicno { get; set; }
        public string NicfrontImage { get; set; }
        public string NicbackImage { get; set; }
        public string DrivingLicenceNo { get; set; }
        public string DrivingLicenceFrontImage { get; set; }
        public string DrivingLicenceBackImage { get; set; }
        public string PersonalAddress { get; set; }
        public bool? IsActive { get; set; }

        public virtual TimeZoneDetail TimeZone { get; set; }
        public virtual ICollection<Client> ClientCreatedBies { get; set; }
        public virtual ICollection<Client> ClientUpdatedBies { get; set; }
        public virtual ICollection<CustomerProductPrice> CustomerProductPriceCreatedBies { get; set; }
        public virtual ICollection<CustomerProductPrice> CustomerProductPriceUpdatedBies { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeatCreatedByNavigations { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeatDrivers { get; set; }
        public virtual ICollection<DailyVehicleBeatOrder> DailyVehicleBeatOrders { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeatSalesReps { get; set; }
        public virtual ICollection<DailyVehicleBeat> DailyVehicleBeatUpdatedByNavigations { get; set; }
        public virtual ICollection<Expense> ExpenseCreatedBies { get; set; }
        public virtual ICollection<Expense> ExpenseUpdatedBies { get; set; }
        public virtual ICollection<Product> ProductCreatedBies { get; set; }
        public virtual ICollection<ProductInventory> ProductInventoryCreatedBies { get; set; }
        public virtual ICollection<ProductInventoryOrder> ProductInventoryOrderCreatedBies { get; set; }
        public virtual ICollection<ProductInventoryOrder> ProductInventoryOrderUpdatedBies { get; set; }
        public virtual ICollection<ProductInventory> ProductInventoryUdatedBies { get; set; }
        public virtual ICollection<ProductReturn> ProductReturnCreatedBies { get; set; }
        public virtual ICollection<ProductReturn> ProductReturnUpdatedBies { get; set; }
        public virtual ICollection<Product> ProductUpdatedBies { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderCreatedBies { get; set; }
        public virtual ICollection<PurchaseOrderSendingHistory> PurchaseOrderSendingHistories { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderUpdatedBies { get; set; }
        public virtual ICollection<SalesOrder> SalesOrderCreatedBies { get; set; }
        public virtual ICollection<SalesOrder> SalesOrderUpdatedBies { get; set; }
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
