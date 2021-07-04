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

    public virtual DbSet<AppSetting> AppSettings { get; set; }
    public virtual DbSet<BreakOilCode> BreakOilCodes { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<CustomerProductPrice> CustomerProductPrices { get; set; }
    public virtual DbSet<DailyVehicleBeat> DailyVehicleBeats { get; set; }
    public virtual DbSet<DailyVehicleBeatOrder> DailyVehicleBeatOrders { get; set; }
    public virtual DbSet<DifferentialOilCode> DifferentialOilCodes { get; set; }
    public virtual DbSet<EgineCoolant> EgineCoolants { get; set; }
    public virtual DbSet<EngineOilCode> EngineOilCodes { get; set; }
    public virtual DbSet<GearBoxOilCode> GearBoxOilCodes { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<PowerSteeringOilCode> PowerSteeringOilCodes { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<ProductImage> ProductImages { get; set; }
    public virtual DbSet<ProductInventory> ProductInventories { get; set; }
    public virtual DbSet<ProductReturn> ProductReturns { get; set; }
    public virtual DbSet<ProductSubCategory> ProductSubCategories { get; set; }
    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    public virtual DbSet<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Route> Routes { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<TimeZoneDetail> TimeZoneDetails { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<Vehicle> Vehicles { get; set; }
    public virtual DbSet<VehicleAirCleaner> VehicleAirCleaners { get; set; }
    public virtual DbSet<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilages { get; set; }
    public virtual DbSet<VehicleEmissiontTest> VehicleEmissiontTests { get; set; }
    public virtual DbSet<VehicleEngineOilMilage> VehicleEngineOilMilages { get; set; }
    public virtual DbSet<VehicleExpense> VehicleExpenses { get; set; }
    public virtual DbSet<VehicleFitnessReport> VehicleFitnessReports { get; set; }
    public virtual DbSet<VehicleFuelFilterMilage> VehicleFuelFilterMilages { get; set; }
    public virtual DbSet<VehicleGearBoxOilMilage> VehicleGearBoxOilMilages { get; set; }
    public virtual DbSet<VehicleGreeceNiple> VehicleGreeceNiples { get; set; }
    public virtual DbSet<VehicleInsurance> VehicleInsurances { get; set; }
    public virtual DbSet<VehicleRevenueLicence> VehicleRevenueLicences { get; set; }
    public virtual DbSet<VehicleType> VehicleTypes { get; set; }
    public virtual DbSet<Wharehouse> Wharehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        optionsBuilder.UseSqlServer("Server=DESKTOP-4B3H53F;Database=VMDB;Trusted_Connection=True;User Id=sa;Password=1qaz2wsx@;");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

      modelBuilder.Entity<AppSetting>(entity =>
      {
        entity.Property(e => e.Key)
            .IsRequired()
            .HasMaxLength(500);
      });

      modelBuilder.Entity<BreakOilCode>(entity =>
      {
        entity.Property(e => e.Code).HasMaxLength(50);
      });

      modelBuilder.Entity<Client>(entity =>
      {
        entity.ToTable("Client");

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
            .WithMany(p => p.ClientCreatedBies)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Client_User");

        entity.HasOne(d => d.Route)
            .WithMany(p => p.Clients)
            .HasForeignKey(d => d.RouteId)
            .HasConstraintName("FK_Client_Route");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.ClientUpdatedBies)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Client_User1");
      });

      modelBuilder.Entity<CustomerProductPrice>(entity =>
      {
        entity.HasKey(e => new { e.CustomerId, e.ProductId });

        entity.ToTable("CustomerProductPrice");

        entity.Property(e => e.AssignedUnitPrice).HasColumnType("decimal(18, 2)");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.CustomerProductPriceCreatedBies)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerProductPrice_User");

        entity.HasOne(d => d.Customer)
            .WithMany(p => p.CustomerProductPrices)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerProductPrice_Client");

        entity.HasOne(d => d.Product)
            .WithMany(p => p.CustomerProductPrices)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerProductPrice_Product");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.CustomerProductPriceUpdatedBies)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CustomerProductPrice_User1");
      });

      modelBuilder.Entity<DailyVehicleBeat>(entity =>
      {
        entity.ToTable("DailyVehicleBeat");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.Date).HasColumnType("datetime");

        entity.Property(e => e.EndMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.StartingMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.DailyVehicleBeatCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyVehicleBeat_User");

        entity.HasOne(d => d.Route)
            .WithMany(p => p.DailyVehicleBeats)
            .HasForeignKey(d => d.RouteId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyVehicleBeat_Route");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.DailyVehicleBeatUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyVehicleBeat_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.DailyVehicleBeats)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DailyVehicleBeat_Vehicle");
      });

      modelBuilder.Entity<DailyVehicleBeatOrder>(entity =>
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

      modelBuilder.Entity<DifferentialOilCode>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<EgineCoolant>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<EngineOilCode>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<GearBoxOilCode>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<Order>(entity =>
      {
        entity.ToTable("Order");

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
            .WithMany(p => p.OrderCreatedBies)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_User");

        entity.HasOne(d => d.Owner)
            .WithMany(p => p.Orders)
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_Client");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.OrderUpdatedBies)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_User1");
      });

      modelBuilder.Entity<OrderItem>(entity =>
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

      modelBuilder.Entity<PowerSteeringOilCode>(entity =>
      {
        entity.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);
      });

      modelBuilder.Entity<Product>(entity =>
      {
        entity.ToTable("Product");

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
            .WithMany(p => p.ProductCreatedBies)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Product_User");

        entity.HasOne(d => d.SubProductCategory)
            .WithMany(p => p.Products)
            .HasForeignKey(d => d.SubProductCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Product_ProductSubCategory");

        entity.HasOne(d => d.Supplier)
            .WithMany(p => p.Products)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Product_Supplier");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.ProductUpdatedBies)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Product_User1");
      });

      modelBuilder.Entity<ProductCategory>(entity =>
      {
        entity.ToTable("ProductCategory");

        entity.Property(e => e.Description).HasMaxLength(1000);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);
      });

      modelBuilder.Entity<ProductImage>(entity =>
      {
        entity.ToTable("ProductImage");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(500);

        entity.HasOne(d => d.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(d => d.ProductId)
            .HasConstraintName("FK_ProductImage_ProductImage");
      });

      modelBuilder.Entity<ProductInventory>(entity =>
      {
        entity.ToTable("ProductInventory");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.DateRecieved).HasColumnType("datetime");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.ProductInventoryCreatedBies)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductInventory_User");

        entity.HasOne(d => d.Product)
            .WithMany(p => p.ProductInventories)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductInventory_Product");

        entity.HasOne(d => d.UdatedBy)
            .WithMany(p => p.ProductInventoryUdatedBies)
            .HasForeignKey(d => d.UdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductInventory_User1");
      });

      modelBuilder.Entity<ProductReturn>(entity =>
      {
        entity.ToTable("ProductReturn");

        entity.Property(e => e.ReturnDate).HasColumnType("datetime");
      });

      modelBuilder.Entity<ProductSubCategory>(entity =>
      {
        entity.ToTable("ProductSubCategory");

        entity.Property(e => e.Description).HasMaxLength(1000);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);

        entity.HasOne(d => d.ProductCategory)
            .WithMany(p => p.ProductSubCategories)
            .HasForeignKey(d => d.ProductCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProductSubCategory_ProductCategory");
      });

      modelBuilder.Entity<PurchaseOrder>(entity =>
      {
        entity.ToTable("PurchaseOrder");

        entity.Property(e => e.Id).ValueGeneratedNever();

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.Discount).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Ponumber)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnName("PONumber");

        entity.Property(e => e.ShipingCharge).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.TaxRate).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.TotalTaxAmount).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.PurchaseOrderCreatedBies)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrder_User");

        entity.HasOne(d => d.ShippedToWharehouse)
            .WithMany(p => p.PurchaseOrders)
            .HasForeignKey(d => d.ShippedToWharehouseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrder_Wharehouse");

        entity.HasOne(d => d.Supplier)
            .WithMany(p => p.PurchaseOrders)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrder_Supplier");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.PurchaseOrderUpdatedBies)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrder_User1");
      });

      modelBuilder.Entity<PurchaseOrderDetail>(entity =>
      {
        entity.ToTable("PurchaseOrderDetail");

        entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

        entity.HasOne(d => d.Product)
            .WithMany(p => p.PurchaseOrderDetails)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrderDetail_Product");

        entity.HasOne(d => d.PurchaseOrder)
            .WithMany(p => p.PurchaseOrderDetails)
            .HasForeignKey(d => d.PurchaseOrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PurchaseOrderDetail_PurchaseOrder");
      });

      modelBuilder.Entity<PurchaseOrderPayment>(entity =>
      {
        entity.ToTable("PurchaseOrderPayment");

        entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.PaymentDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
      });

      modelBuilder.Entity<Role>(entity =>
      {
        entity.ToTable("Role");

        entity.Property(e => e.Name).HasMaxLength(50);
      });

      modelBuilder.Entity<Route>(entity =>
      {
        entity.ToTable("Route");

        entity.Property(e => e.EndFrom)
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Name).HasMaxLength(350);

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
        entity.ToTable("Supplier");

        entity.Property(e => e.AccountNo).HasMaxLength(50);

        entity.Property(e => e.Address).HasMaxLength(1000);

        entity.Property(e => e.Bank).HasMaxLength(500);

        entity.Property(e => e.Branch).HasMaxLength(500);

        entity.Property(e => e.BranchCode).HasMaxLength(20);

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.Email1).HasMaxLength(150);

        entity.Property(e => e.Email2).HasMaxLength(150);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(500);

        entity.Property(e => e.OurRefNo).HasMaxLength(50);

        entity.Property(e => e.Phone1).HasMaxLength(15);

        entity.Property(e => e.Phone2).HasMaxLength(15);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.SupplierCreatedBies)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Supplier_User");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.SupplierUpdatedBies)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Supplier_User1");
      });

      modelBuilder.Entity<TimeZoneDetail>(entity =>
      {
        entity.Property(e => e.DisplayName)
            .IsRequired()
            .HasMaxLength(500);

        entity.Property(e => e.TimeZoneId)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("TimeZoneID");
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.ToTable("User");

        entity.Property(e => e.DrivingLicenceBackImage).HasMaxLength(500);

        entity.Property(e => e.DrivingLicenceFrontImage).HasMaxLength(500);

        entity.Property(e => e.DrivingLicenceNo).HasMaxLength(20);

        entity.Property(e => e.Email).HasMaxLength(150);

        entity.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(e => e.Image).HasMaxLength(500);

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(e => e.MobileNo).HasMaxLength(12);

        entity.Property(e => e.NicbackImage)
            .HasMaxLength(500)
            .HasColumnName("NICBackImage");

        entity.Property(e => e.NicfrontImage)
            .HasMaxLength(500)
            .HasColumnName("NICFrontImage");

        entity.Property(e => e.Nicno)
            .HasMaxLength(20)
            .HasColumnName("NICNo");

        entity.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.PersonalAddress).HasMaxLength(1000);

        entity.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(150);

        entity.HasOne(d => d.TimeZone)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.TimeZoneId)
            .HasConstraintName("FK_User_TimeZoneDetails");
      });

      modelBuilder.Entity<UserRole>(entity =>
      {
        entity.HasKey(e => new { e.UserId, e.RoleId });

        entity.ToTable("UserRole");

        entity.Property(e => e.EndDate).HasColumnType("datetime");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.StartedDate).HasColumnType("datetime");

        entity.HasOne(d => d.Role)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRole_Role");

        entity.HasOne(d => d.User)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRole_User");
      });

      modelBuilder.Entity<Vehicle>(entity =>
      {
        entity.ToTable("Vehicle");

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
            .WithMany(p => p.Vehicles)
            .HasForeignKey(d => d.VehicelTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Vehicle_VehicleType");
      });

      modelBuilder.Entity<VehicleAirCleaner>(entity =>
      {
        entity.ToTable("VehicleAirCleaner");

        entity.Property(e => e.AirCleanerReplaceMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.NextAirCleanerReplaceMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleAirCleanerCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleAirCleaner_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleAirCleanerUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleAirCleaner_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleAirCleaners)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleAirCleaner_Vehicle");
      });

      modelBuilder.Entity<VehicleDifferentialOilChangeMilage>(entity =>
      {
        entity.ToTable("VehicleDifferentialOilChangeMilage");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.DifferentialOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.NextDifferentialOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleDifferentialOilChangeMilageCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleDifferentialOilChangeMilage_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleDifferentialOilChangeMilageUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleDifferentialOilChangeMilage_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleDifferentialOilChangeMilages)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleDifferentialOilChangeMilage_Vehicle");
      });

      modelBuilder.Entity<VehicleEmissiontTest>(entity =>
      {
        entity.ToTable("VehicleEmissiontTest");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.EmissiontTestDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.Property(e => e.ValidTill).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleEmissiontTestCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEmissiontTest_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleEmissiontTestUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEmissiontTest_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleEmissiontTests)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEmissiontTest_Vehicle");
      });

      modelBuilder.Entity<VehicleEngineOilMilage>(entity =>
      {
        entity.ToTable("VehicleEngineOilMilage");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.NextOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.OilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleEngineOilMilageCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEngineOilMilage_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleEngineOilMilageUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEngineOilMilage_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleEngineOilMilages)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleEngineOilMilage_Vehicle");
      });

      modelBuilder.Entity<VehicleExpense>(entity =>
      {
        entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.Date).HasColumnType("datetime");

        entity.Property(e => e.Description).HasMaxLength(250);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleExpenseCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleExpenses_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleExpenseUpdatedByNavigations)
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
        entity.ToTable("VehicleFitnessReport");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.FitnessReportDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.Property(e => e.ValidTill).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleFitnessReportCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFitnessReport_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleFitnessReportUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFitnessReport_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleFitnessReports)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFitnessReport_Vehicle");
      });

      modelBuilder.Entity<VehicleFuelFilterMilage>(entity =>
      {
        entity.ToTable("VehicleFuelFilterMilage");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.FuelFilterChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");

        entity.Property(e => e.NextFuelFilterChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleFuelFilterMilageCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFuelFilterMilage_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleFuelFilterMilageUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFuelFilterMilage_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleFuelFilterMilages)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleFuelFilterMilage_Vehicle");
      });

      modelBuilder.Entity<VehicleGearBoxOilMilage>(entity =>
      {
        entity.ToTable("VehicleGearBoxOilMilage");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.GearBoxOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.NextGearBoxOilChangeMilage).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleGearBoxOilMilageCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGearBoxOilMilage_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleGearBoxOilMilageUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGearBoxOilMilage_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleGearBoxOilMilages)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGearBoxOilMilage_Vehicle");
      });

      modelBuilder.Entity<VehicleGreeceNiple>(entity =>
      {
        entity.ToTable("VehicleGreeceNiple");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.GreeceNipleReplaceDate).HasColumnType("datetime");

        entity.Property(e => e.NextGreeceNipleReplaceDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleGreeceNipleCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGreeceNiple_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleGreeceNipleUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGreeceNiple_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleGreeceNiples)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleGreeceNiple_Vehicle");
      });

      modelBuilder.Entity<VehicleInsurance>(entity =>
      {
        entity.ToTable("VehicleInsurance");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.InsuranceDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.Property(e => e.ValidTill).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleInsuranceCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleInsurance_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleInsuranceUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleInsurance_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleInsurances)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleInsurance_Vehicle");
      });

      modelBuilder.Entity<VehicleRevenueLicence>(entity =>
      {
        entity.ToTable("VehicleRevenueLicence");

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.RevenueLicenceDate).HasColumnType("datetime");

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.Property(e => e.ValidTill).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedByNavigation)
            .WithMany(p => p.VehicleRevenueLicenceCreatedByNavigations)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleRevenueLicence_User");

        entity.HasOne(d => d.UpdatedByNavigation)
            .WithMany(p => p.VehicleRevenueLicenceUpdatedByNavigations)
            .HasForeignKey(d => d.UpdatedBy)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleRevenueLicence_User1");

        entity.HasOne(d => d.Vehicle)
            .WithMany(p => p.VehicleRevenueLicences)
            .HasForeignKey(d => d.VehicleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VehicleRevenueLicence_Vehicle");
      });

      modelBuilder.Entity<VehicleType>(entity =>
      {
        entity.ToTable("VehicleType");

        entity.Property(e => e.FuelFilterNumber).HasMaxLength(50);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        entity.HasOne(d => d.BreakOil)
            .WithMany(p => p.VehicleTypes)
            .HasForeignKey(d => d.BreakOilId)
            .HasConstraintName("FK_VehicleType_BreakOilCodes");

        entity.HasOne(d => d.DifferentialOil)
            .WithMany(p => p.VehicleTypes)
            .HasForeignKey(d => d.DifferentialOilId)
            .HasConstraintName("FK_VehicleType_DifferentialOilCodes");

        entity.HasOne(d => d.EngineCoolant)
            .WithMany(p => p.VehicleTypes)
            .HasForeignKey(d => d.EngineCoolantId)
            .HasConstraintName("FK_VehicleType_EgineCoolants");

        entity.HasOne(d => d.EngineOil)
            .WithMany(p => p.VehicleTypes)
            .HasForeignKey(d => d.EngineOilId)
            .HasConstraintName("FK_VehicleType_EngineOilCodes");

        entity.HasOne(d => d.GearBoxOil)
            .WithMany(p => p.VehicleTypes)
            .HasForeignKey(d => d.GearBoxOilId)
            .HasConstraintName("FK_VehicleType_GearBoxOilCodes");

        entity.HasOne(d => d.PowerSteeringOil)
            .WithMany(p => p.VehicleTypes)
            .HasForeignKey(d => d.PowerSteeringOilId)
            .HasConstraintName("FK_VehicleType_PowerSteeringOilCodes");
      });

      modelBuilder.Entity<Wharehouse>(entity =>
      {
        entity.ToTable("Wharehouse");

        entity.Property(e => e.Address).IsRequired();

        entity.Property(e => e.CreatedOn).HasColumnType("datetime");

        entity.Property(e => e.FloorSpace).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.Phone).HasMaxLength(15);

        entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        entity.HasOne(d => d.CreatedBy)
            .WithMany(p => p.WharehouseCreatedBies)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Wharehouse_User");

        entity.HasOne(d => d.Manager)
            .WithMany(p => p.WharehouseManagers)
            .HasForeignKey(d => d.ManagerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Wharehouse_Wharehouse");

        entity.HasOne(d => d.UpdatedBy)
            .WithMany(p => p.WharehouseUpdatedBies)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Wharehouse_User1");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
