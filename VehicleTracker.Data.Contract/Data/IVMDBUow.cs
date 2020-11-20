using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;

namespace VehicleTracker.Data
{
    public interface IVMDBUow
    {
        void Commit();
        Task<int> CommitAsync();

        void BeginTransaction();
        void EndTransaction();
        void RolebackTransaction();


        IRepository<BreakOilCodes> BreakOilCodes { get;  }
        IRepository<DifferentialOilCodes> DifferentialOilCodes { get; }
        IRepository<EgineCoolants> EgineCoolants { get;  }
        IRepository<EngineOilCodes> EngineOilCodes { get;  }
        IRepository<GearBoxOilCodes> GearBoxOilCodes { get;  }
        IRepository<PowerSteeringOilCodes> PowerSteeringOilCodes { get; }
        IRepository<DailyVehicleBeat> DailyVehicleBeat { get;  }
        IRepository<Role> Role { get;  }
        IRepository<Route> Route { get;  }
        IRepository<User> User { get; }
        IRepository<UserRole> UserRole { get; }
        IRepository<Vehicle> Vehicle { get;  }
        IRepository<VehicleAirCleaner> VehicleAirCleaner { get; }
        IRepository<VehicleDifferentialOilChangeMilage> VehicleDifferentialOilChangeMilage { get;  }
        IRepository<VehicleEmissiontTest> VehicleEmissiontTest { get;  }
        IRepository<VehicleEngineOilMilage> VehicleEngineOilMilage { get;  }
        IRepository<VehicleFitnessReport> VehicleFitnessReport { get;  }
        IRepository<VehicleFuelFilterMilage> VehicleFuelFilterMilage { get;  }
        IRepository<VehicleGearBoxOilMilage> VehicleGearBoxOilMilage { get;  }
        IRepository<VehicleGreeceNiple> VehicleGreeceNiple { get;  }
        IRepository<VehicleInsurance> VehicleInsurance { get;  }
        IRepository<VehicleRevenueLicence> VehicleRevenueLicence { get;  }
        IRepository<VehicleType> VehicleType { get;  }
        IRepository<VehicleExpenses> VehicleExpense { get; }
    }
}
