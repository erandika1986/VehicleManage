using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VehicleTracker.Model
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
        public virtual DbSet<DailyVehicleBeat> DailyVehicleBeat { get; set; }
        public virtual DbSet<DifferentialOilCodes> DifferentialOilCodes { get; set; }
        public virtual DbSet<EgineCoolants> EgineCoolants { get; set; }
        public virtual DbSet<EngineOilCodes> EngineOilCodes { get; set; }
        public virtual DbSet<GearBoxOilCodes> GearBoxOilCodes { get; set; }
        public virtual DbSet<PowerSteeringOilCodes> PowerSteeringOilCodes { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Route> Route { get; set; }
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

            modelBuilder.Entity<PowerSteeringOilCodes>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);
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
                entity.Property(e => e.ActualAirCleanerReplaceMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextAirCleanerReplaceMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VehicleAirCleanerCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleAirCleaner_User");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleAirCleaner_VehicleAirCleaner");

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
                entity.Property(e => e.ActualDifferentialOilChangeMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextDifferentialOilChangeMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleDifferentialOilChangeMilage_VehicleDifferentialOilChangeMilage");

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
                entity.Property(e => e.ActualEmissiontTestDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextEmissiontTestDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleEmissiontTest_VehicleEmissiontTest");

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
                entity.Property(e => e.ActualOilChangeMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextOilChangeMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VehicleEngineOilMilageCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleEngineOilMilage_User");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleEngineOilMilage_VehicleEngineOilMilage");

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
                entity.Property(e => e.ActualFitnessReportDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextFitnessReportDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleFitnessReport_VehicleFitnessReport");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleFitnessReport)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleFitnessReport_Vehicle");
            });

            modelBuilder.Entity<VehicleFuelFilterMilage>(entity =>
            {
                entity.Property(e => e.ActualFuelFilterChangeMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

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

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleFuelFilterMilage_VehicleFuelFilterMilage");

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
                entity.Property(e => e.ActualGearBoxOilChangeMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextGearBoxOilChangeMilage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VehicleGearBoxOilMilageCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleGearBoxOilMilage_User");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleGearBoxOilMilage_VehicleGearBoxOilMilage");

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
                entity.Property(e => e.ActualGreeceNipleReplaceDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextGreeceNipleReplaceDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VehicleGreeceNipleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleGreeceNiple_User");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleGreeceNiple_VehicleGreeceNiple");

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
                entity.Property(e => e.ActualInsuranceDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextInsuranceDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VehicleInsuranceCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleInsurance_User");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleInsurance_VehicleInsurance");

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
                entity.Property(e => e.ActualRevenueLicenceDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.NextRevenueLicenceDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VehicleRevenueLicenceCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleRevenueLicence_User");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_VehicleRevenueLicence_VehicleRevenueLicence");

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
        }
    }
}
