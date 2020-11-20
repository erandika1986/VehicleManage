using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;

namespace VehicleTracker.Data
{
    public class VMDBUow: IVMDBUow
    {
        #region Member Variables

        private IRepositoryProvider repositoryProvider;
        private VMDBContext dbContext;
        private IDbContextTransaction transaction;

        #endregion

        #region Constructor

        public VMDBUow(VMDBContext vMDBContext)
        {
            dbContext = vMDBContext;
            this.repositoryProvider = new RepositoryProvider(dbContext, new RepositoryFactories());
        }

        #endregion

        public IRepository<BreakOilCodes> BreakOilCodes { get { return GetRepositoryByModel<BreakOilCodes>(); } }
        public IRepository<DifferentialOilCodes> DifferentialOilCodes { get { return GetRepositoryByModel<DifferentialOilCodes>(); } }
        public IRepository<EgineCoolants> EgineCoolants { get { return GetRepositoryByModel<EgineCoolants>(); } }
        public IRepository<EngineOilCodes> EngineOilCodes { get { return GetRepositoryByModel<EngineOilCodes>(); } }
        public IRepository<GearBoxOilCodes> GearBoxOilCodes { get { return GetRepositoryByModel<GearBoxOilCodes>(); } }
        public IRepository<PowerSteeringOilCodes> PowerSteeringOilCodes { get { return GetRepositoryByModel<PowerSteeringOilCodes>(); } }
        public IRepository<DailyVehicleBeat> DailyVehicleBeat { get { return GetRepositoryByModel<DailyVehicleBeat>(); } }
        public IRepository<Role> Role { get { return GetRepositoryByModel<Role>(); } }
        public IRepository<Route> Route { get { return GetRepositoryByModel<Route>(); } }
        public IRepository<User> User { get { return GetRepositoryByModel<User>(); } }
        public IRepository<UserRole> UserRole { get { return GetRepositoryByModel<UserRole>(); } }
        public IRepository<Vehicle> Vehicle { get { return GetRepositoryByModel<Vehicle>(); } }
        public IRepository<VehicleAirCleaner> VehicleAirCleaner { get { return GetRepositoryByModel<VehicleAirCleaner>(); } }
        public IRepository<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilage { get { return GetRepositoryByModel<VehicleDifferentialOilChangeMilage>(); } }
        public IRepository<VehicleEmissiontTest> VehicleEmissiontTest { get { return GetRepositoryByModel<VehicleEmissiontTest>(); } }
        public IRepository<VehicleEngineOilMilage> VehicleEngineOilMilage { get { return GetRepositoryByModel<VehicleEngineOilMilage>(); } }
        public IRepository<VehicleFitnessReport> VehicleFitnessReport { get { return GetRepositoryByModel<VehicleFitnessReport>(); } }
        public IRepository<VehicleFuelFilterMilage> VehicleFuelFilterMilage { get { return GetRepositoryByModel<VehicleFuelFilterMilage>(); } }
        public IRepository<VehicleGearBoxOilMilage> VehicleGearBoxOilMilage { get { return GetRepositoryByModel<VehicleGearBoxOilMilage>(); } }
        public IRepository<VehicleGreeceNiple> VehicleGreeceNiple { get { return GetRepositoryByModel<VehicleGreeceNiple>(); } }
        public IRepository<VehicleInsurance> VehicleInsurance { get { return GetRepositoryByModel<VehicleInsurance>(); } }
        public IRepository<VehicleRevenueLicence> VehicleRevenueLicence { get { return GetRepositoryByModel<VehicleRevenueLicence>(); } }
        public IRepository<VehicleType> VehicleType { get { return GetRepositoryByModel<VehicleType>(); } }
        public IRepository<VehicleExpenses> VehicleExpense { get { return GetRepositoryByModel<VehicleExpenses>(); } }



        #region Public Methods

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            transaction = dbContext.Database.BeginTransaction();
        }

        public void EndTransaction()
        {
            if (transaction != null)
                transaction.Commit();
        }

        public void RolebackTransaction()
        {
            if (transaction != null)
                transaction.Rollback();
        }

        #endregion

        #region Private Methods

        private IRepository<T> GetRepositoryByModel<T>() where T : class
        {
            return repositoryProvider.GetRepositoryByEntity<T>();
        }

        private T GetRepositoryByRepository<T>() where T : class
        {
            return repositoryProvider.GetRepositoryByRepository<T>();
        }

        #endregion
    }
}
