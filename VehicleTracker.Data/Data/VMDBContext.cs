using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VehicleTracker.Model;

namespace VehicleTracker.Data
{
  public partial class VMDBContext : DbContext
  {
    public VMDBContext()
    {
    }

    public VMDBContext(DbContextOptions<VMDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BreakOilCodes> BreakOilCodes { get; set; }
    public virtual DbSet<Client> Client { get; set; }
    public virtual DbSet<CustomerProductPrice> CustomerProductPrice { get; set; }
    public virtual DbSet<DailyVehicleBeat> DailyVehicleBeat { get; set; }
    public virtual DbSet<DailyVehicleBeatOrders> DailyVehicleBeatOrders { get; set; }
    public virtual DbSet<DifferentialOilCodes> DifferentialOilCodes { get; set; }
    public virtual DbSet<EgineCoolants> EgineCoolants { get; set; }
    public virtual DbSet<EngineOilCodes> EngineOilCodes { get; set; }
    public virtual DbSet<GearBoxOilCodes> GearBoxOilCodes { get; set; }
    public virtual DbSet<Order> Order { get; set; }
    public virtual DbSet<OrderItems> OrderItems { get; set; }
    public virtual DbSet<PowerSteeringOilCodes> PowerSteeringOilCodes { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<ProductCategory> ProductCategory { get; set; }
    public virtual DbSet<ProductInventory> ProductInventory { get; set; }
    public virtual DbSet<ProductReturn> ProductReturn { get; set; }
    public virtual DbSet<ProductSubCategory> ProductSubCategory { get; set; }
    public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
    public virtual DbSet<PurchaseOrderPayment> PurchaseOrderPayment { get; set; }
    public virtual DbSet<Role> Role { get; set; }
    public virtual DbSet<Route> Route { get; set; }
    public virtual DbSet<Supplier> Supplier { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<UserRole> UserRole { get; set; }
    public virtual DbSet<Vehicle> Vehicle { get; set; }
    public virtual DbSet<VehicleAirCleaner> VehicleAirCleaner { get; set; }
    public virtual DbSet<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilage { get; set; }
    public virtual DbSet<VehicleEmissiontTest> VehicleEmissiontTest { get; set; }
    public virtual DbSet<VehicleEngineOilMilage> VehicleEngineOilMilage { get; set; }
    public virtual DbSet<VehicleExpenses> VehicleExpenses { get; set; }
    public virtual DbSet<VehicleFitnessReport> VehicleFitnessReport { get; set; }
    public virtual DbSet<VehicleFuelFilterMilage> VehicleFuelFilterMilage { get; set; }
    public virtual DbSet<VehicleGearBoxOilMilage> VehicleGearBoxOilMilage { get; set; }
    public virtual DbSet<VehicleGreeceNiple> VehicleGreeceNiple { get; set; }
    public virtual DbSet<VehicleInsurance> VehicleInsurance { get; set; }
    public virtual DbSet<VehicleRevenueLicence> VehicleRevenueLicence { get; set; }
    public virtual DbSet<VehicleType> VehicleType { get; set; }
    public virtual DbSet<Wharehouse> Wharehouse { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        optionsBuilder.UseSqlServer("Server=DESKTOP-4B3H53F;Database=VMDB;Trusted_Connection=True;User Id=sa;Password=1qaz2wsx@;");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

      modelBuilder.Entity<BreakOilCodes>(entity =>
      {
        entity.Property(e => e.Code).HasMaxLength(50);
      });

      modelBuilder.Entity<Client>(entity =>
      {
        entity.Property(e => e.Address).HasMaxLength(500);

        entity.Property(e => e.ContactNo1).HasMaxLength(15);

        entity.Property(e => e.ContactNo2).HasMaxLength(15);

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.Description).HasMaxLength(1500);

        entity.Property(e => e.Email).HasMaxLength(100);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Latitude).HasColumnType("decimal(18, 4)");

        entity.Property(e => e.Longitude).HasColumnType("decimal(18, 4)");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(500);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.ClientCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Client_User");

        entity.HasOne(d => d.Route)
            .WithMany(p => p.Client)
            .HasForeignKey(d => d.RouteId)
            .HasConstraintName("FK_Client_Route");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.ClientUpdatedBy)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Client_User1");
      });

      modelBuilder.Entity<CustomerProductPrice>(entity =>
      {
        entity.HasKey(e => new { e.CustomerId, e.ProductId });

        entity.Property(e => e.AssignedUnitPrice).HasColumnType("decimal(18, 2)");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.CustomerProductPriceCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerProductPrice_User");

        entity.HasOne(d => d.Customer)
            .WithMany(p => p.CustomerProductPrice)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerProductPrice_Client");

        entity.HasOne(d => d.Product)
            .WithMany(p => p.CustomerProductPrice)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerProductPrice_Product");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.CustomerProductPriceUpdatedBy)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerProductPrice_User1");
      });

      modelBuilder.Entity<DailyVehicleBeat>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.Date).HasColumnType("datetime");

        entity.Property(e => e.EndMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.StartingMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.DailyVehicleBeatCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyVehicleBeat_User");

        entity.HasOne(d => d.Route)
            .WithMany(p => p.DailyVehicleBeat)
            .HasForeignKey(d => d.RouteId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyVehicleBeat_Route");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.DailyVehicleBeatUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyVehicleBeat_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.DailyVehicleBeat)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyVehicleBeat_Vehicle");
      });

      modelBuilder.Entity<DailyVehicleBeatOrders>(entity =>
      {
        entity.HasOne(d => d.DailyVehicleBeat)
            .WithMany(p => p.DailyVehicleBeatOrders)
            .HasForeignKey(d => d.DailyVehicleBeatId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyBeatOrders_DailyVehicleBeat");

        entity.HasOne(d => d.Order)
            .WithMany(p => p.DailyVehicleBeatOrders)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyBeatOrders_Order");
      });

      modelBuilder.Entity<DifferentialOilCodes>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<EgineCoolants>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<EngineOilCodes>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<GearBoxOilCodes>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<Order>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.DeliveredDate).HasColumnType("datetime");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.OrderDate).HasColumnType("datetime");

        entity.Property(e => e.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.OrderCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_User");

        entity.HasOne(d => d.Owner)
            .WithMany(p => p.Order)
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_Client");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.OrderUpdatedBy)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_User1");
      });

      modelBuilder.Entity<OrderItems>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

        entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

        entity.HasOne(d => d.Order)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OrderItems_Order");
      });

      modelBuilder.Entity<PowerSteeringOilCodes>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<Product>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.ProductCode)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(e => e.ProductName)
            .IsRequired()
            .HasMaxLength(500);

        entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.ProductCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Product_User");

        entity.HasOne(d => d.SubProductCategory)
            .WithMany(p => p.Product)
            .HasForeignKey(d => d.SubProductCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Product_ProductSubCategory");

        entity.HasOne(d => d.Supplier)
            .WithMany(p => p.Product)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Product_Supplier");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.ProductUpdatedBy)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Product_User1");
      });

      modelBuilder.Entity<ProductCategory>(entity =>
      {
        entity.Property(e => e.Description).HasMaxLength(1000);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);
      });

      modelBuilder.Entity<ProductInventory>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.DateRecieved).HasColumnType("datetime");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.ProductInventoryCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductInventory_User");

        entity.HasOne(d => d.Product)
            .WithMany(p => p.ProductInventory)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductInventory_Product");

        entity.HasOne(d => d.UdatedBy)
            .WithMany(p => p.ProductInventoryUdatedBy)
            .HasForeignKey(d => d.UdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductInventory_User1");
      });

      modelBuilder.Entity<ProductReturn>(entity =>
      {
        entity.Property(e => e.ReturnDate).HasColumnType("datetime");
      });

      modelBuilder.Entity<ProductSubCategory>(entity =>
      {
        entity.Property(e => e.Description).HasMaxLength(1000);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);

        entity.HasOne(d => d.ProductCategory)
            .WithMany(p => p.ProductSubCategory)
            .HasForeignKey(d => d.ProductCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductSubCategory_ProductCategory");
      });

      modelBuilder.Entity<PurchaseOrder>(entity =>
      {
        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.Amount)
            .IsRequired()
            .HasMaxLength(10);

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Ponumber)
            .IsRequired()
            .HasColumnName("PONumber")
            .HasMaxLength(15);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.PurchaseOrderCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrder_User");

        entity.HasOne(d => d.ShippedToWharehouse)
            .WithMany(p => p.PurchaseOrder)
            .HasForeignKey(d => d.ShippedToWharehouseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrder_Wharehouse");

        entity.HasOne(d => d.Supplier)
            .WithMany(p => p.PurchaseOrder)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrder_Supplier");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.PurchaseOrderUpdatedBy)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrder_User1");
      });

      modelBuilder.Entity<PurchaseOrderDetail>(entity =>
      {
        entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

        entity.HasOne(d => d.PurchaseOrder)
            .WithMany(p => p.PurchaseOrderDetail)
            .HasForeignKey(d => d.PurchaseOrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrderDetail_PurchaseOrder");
      });

      modelBuilder.Entity<PurchaseOrderPayment>(entity =>
      {
        entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.PaymentDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
      });

      modelBuilder.Entity<Role>(entity =>
      {
        entity.Property(e => e.Name).HasMaxLength(50);
      });

      modelBuilder.Entity<Route>(entity =>
      {
        entity.Property(e => e.EndFrom)
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.RouteCode)
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.StartFrom)
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.TotalDistance).HasColumnType("decimal(6, 2)");
      });

      modelBuilder.Entity<Supplier>(entity =>
      {
        entity.Property(e => e.AccountNo).HasMaxLength(50);

        entity.Property(e => e.Address).HasMaxLength(1000);

        entity.Property(e => e.Bank).HasMaxLength(10);

        entity.Property(e => e.Branch).HasMaxLength(10);

        entity.Property(e => e.BranchCode).HasMaxLength(10);

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.Email1).HasMaxLength(150);

        entity.Property(e => e.Email2).HasMaxLength(150);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(500);

        entity.Property(e => e.Phone1).HasMaxLength(15);

        entity.Property(e => e.Phone2).HasMaxLength(15);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.SupplierCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Supplier_User");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.SupplierUpdatedBy)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Supplier_User1");
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(e => e.MobileNo).HasMaxLength(12);

        entity.Property(e => e.Password).HasMaxLength(50);

        entity.Property(e => e.TimeZone).HasMaxLength(500);

        entity.Property(e => e.Username).HasMaxLength(150);
      });

      modelBuilder.Entity<UserRole>(entity =>
      {
        entity.HasKey(e => new { e.UserId, e.RoleId });

        entity.Property(e => e.EndDate).HasColumnType("datetime");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.StartedDate).HasColumnType("datetime");

        entity.HasOne(d => d.Role)
            .WithMany(p => p.UserRole)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRole_Role");

        entity.HasOne(d => d.User)
            .WithMany(p => p.UserRole)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRole_User");
      });

      modelBuilder.Entity<Vehicle>(entity =>
      {
        entity.Property(e => e.CreatedOn)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        entity.Property(e => e.InitialOdometerReading).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.RegistrationNo)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.UpdatedOn)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        entity.HasOne(d => d.VehicelType)
            .WithMany(p => p.Vehicle)
            .HasForeignKey(d => d.VehicelTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Vehicle_VehicleType");
      });

      modelBuilder.Entity<VehicleAirCleaner>(entity =>
      {
        entity.Property(e => e.AirCleanerReplaceMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.NextAirCleanerReplaceMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleAirCleanerCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleAirCleaner_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleAirCleanerUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleAirCleaner_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleAirCleaner)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleAirCleaner_Vehicle");
      });

      modelBuilder.Entity<VehicleDifferentialOilChangeMilage>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.DifferentialOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.NextDifferentialOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleDifferentialOilChangeMilage)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleDifferentialOilChangeMilage_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleDifferentialOilChangeMilage)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleDifferentialOilChangeMilage_Vehicle");
      });

      modelBuilder.Entity<VehicleEmissiontTest>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.EmissiontTestDate).HasColumnType("datetime");

        entity.Property(e => e.NextEmissiontTestDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleEmissiontTest)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEmissiontTest_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleEmissiontTest)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEmissiontTest_Vehicle");
      });

      modelBuilder.Entity<VehicleEngineOilMilage>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.NextOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.OilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleEngineOilMilageCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEngineOilMilage_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleEngineOilMilageUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEngineOilMilage_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleEngineOilMilage)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEngineOilMilage_Vehicle");
      });

      modelBuilder.Entity<VehicleExpenses>(entity =>
      {
        entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.Date).HasColumnType("datetime");

        entity.Property(e => e.Description).HasMaxLength(250);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleExpensesCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleExpenses_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleExpensesUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleExpenses_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleExpenses)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleExpenses_Vehicle");
      });

      modelBuilder.Entity<VehicleFitnessReport>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.FitnessReportDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.Property(e => e.ValidTill).HasColumnType("datetime");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleFitnessReport)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFitnessReport_Vehicle");
      });

      modelBuilder.Entity<VehicleFuelFilterMilage>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.FuelFilterChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.NextFuelFilterChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleFuelFilterMilageCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFuelFilterMilage_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleFuelFilterMilageUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFuelFilterMilage_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleFuelFilterMilage)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFuelFilterMilage_Vehicle");
      });

      modelBuilder.Entity<VehicleGearBoxOilMilage>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.GearBoxOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.NextGearBoxOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleGearBoxOilMilageCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGearBoxOilMilage_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleGearBoxOilMilageUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGearBoxOilMilage_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleGearBoxOilMilage)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGearBoxOilMilage_Vehicle");
      });

      modelBuilder.Entity<VehicleGreeceNiple>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.GreeceNipleReplaceDate).HasColumnType("datetime");

        entity.Property(e => e.NextGreeceNipleReplaceDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleGreeceNipleCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGreeceNiple_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleGreeceNipleUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGreeceNiple_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleGreeceNiple)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGreeceNiple_Vehicle");
      });

      modelBuilder.Entity<VehicleInsurance>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.InsuranceDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.Property(e => e.ValidTill).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleInsuranceCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleInsurance_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleInsuranceUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleInsurance_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleInsurance)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleInsurance_Vehicle");
      });

      modelBuilder.Entity<VehicleRevenueLicence>(entity =>
      {
        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.RevenueLicenceDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.Property(e => e.ValidTill).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleRevenueLicenceCreatedByNavigation)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleRevenueLicence_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleRevenueLicenceUpdatedByNavigation)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleRevenueLicence_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleRevenueLicence)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleRevenueLicence_Vehicle");
      });

      modelBuilder.Entity<VehicleType>(entity =>
      {
        entity.Property(e => e.FuelFilterNumber).HasMaxLength(50);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        entity.HasOne(d => d.BreakOil)
            .WithMany(p => p.VehicleType)
            .HasForeignKey(d => d.BreakOilId)
            .HasConstraintName("FK_VehicleType_BreakOilCodes");

        entity.HasOne(d => d.DifferentialOil)
            .WithMany(p => p.VehicleType)
            .HasForeignKey(d => d.DifferentialOilId)
            .HasConstraintName("FK_VehicleType_DifferentialOilCodes");

        entity.HasOne(d => d.EngineCoolant)
            .WithMany(p => p.VehicleType)
            .HasForeignKey(d => d.EngineCoolantId)
            .HasConstraintName("FK_VehicleType_EgineCoolants");

        entity.HasOne(d => d.EngineOil)
            .WithMany(p => p.VehicleType)
            .HasForeignKey(d => d.EngineOilId)
            .HasConstraintName("FK_VehicleType_EngineOilCodes");

        entity.HasOne(d => d.GearBoxOil)
            .WithMany(p => p.VehicleType)
            .HasForeignKey(d => d.GearBoxOilId)
            .HasConstraintName("FK_VehicleType_GearBoxOilCodes");

        entity.HasOne(d => d.PowerSteeringOil)
            .WithMany(p => p.VehicleType)
            .HasForeignKey(d => d.PowerSteeringOilId)
            .HasConstraintName("FK_VehicleType_PowerSteeringOilCodes");
      });

      modelBuilder.Entity<Wharehouse>(entity =>
      {
        entity.Property(e => e.Address).IsRequired();

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.FloorSpace).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.Phone).HasMaxLength(15);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.WharehouseCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Wharehouse_User");

        entity.HasOne(d => d.Manager)
            .WithMany(p => p.WharehouseManager)
            .HasForeignKey(d => d.ManagerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Wharehouse_Wharehouse");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.WharehouseUpdatedBy)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Wharehouse_User1");
      });
    }
  }
}
