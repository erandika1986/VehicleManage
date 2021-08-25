using Autofac;
using Microsoft.AspNetCore.Http;
using VehicleTracker.Business;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Infrastructure.AutofacModules
{
  public class ApplicationModule
      : Autofac.Module
  {



    public ApplicationModule()
    {

    }

    protected override void Load(ContainerBuilder builder)
    {

      builder.RegisterType<HttpContextAccessor>()
          .As<IHttpContextAccessor>()
          .SingleInstance();

      builder.RegisterType<IdentityService>()
          .As<IIdentityService>()
          .SingleInstance();

      builder.RegisterType<LoggedInUserService>()
          .As<ILoggedInUserService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<DropDownService>()
          .As<IDropDownService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<UserService>()
          .As<IUserService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleService>()
          .As<IVehicleService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<RouteService>()
          .As<IRouteService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleDifferentialOilChangeMilageService>()
          .As<IVehicleDifferentialOilChangeMilageService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleFitnessReportService>()
          .As<IVehicleFitnessReportService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleGreeceNipleService>()
          .As<IVehicleGreeceNipleService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleInsuranceService>()
          .As<IVehicleInsuranceService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleRevenueLicenceService>()
          .As<IVehicleRevenueLicenceService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleEmissionTestService>()
          .As<IVehicleEmissionTestService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleAirCleanerService>()
          .As<IVehicleAirCleanerService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleEngineOilMilageService>()
          .As<IVehicleEngineOilMilageService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleFuelFilterMilageService>()
          .As<IVehicleFuelFilterMilageService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleGearBoxOilMilageService>()
          .As<IVehicleGearBoxOilMilageService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleBeatService>()
          .As<IVehicleBeatService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<CustomerService>()
          .As<ICustomerService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<SupplierService>()
          .As<ISupplierService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<MasterDataCodeSevice>()
          .As<IMasterDataCodeSevice>()
          .InstancePerLifetimeScope();

      builder.RegisterType<VehicleTypeService>()
          .As<IVehicleTypeService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<WarehouseService>()
          .As<IWarehouseService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<ProductCategoryService>()
          .As<IProductCategoryService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<ProductSubCategoryService>()
          .As<IProductSubCategoryService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<ProductService>()
          .As<IProductService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<PurchaseOrderService>()
          .As<IPurchaseOrderService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<InventoryService>()
          .As<IInventoryService>()
          .InstancePerLifetimeScope();

      builder.RegisterType<SaleOrderService>()
          .As<ISaleOrderService>()
          .InstancePerLifetimeScope();

    }
  }
}
