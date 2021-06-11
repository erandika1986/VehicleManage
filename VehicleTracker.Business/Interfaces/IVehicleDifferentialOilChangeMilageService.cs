using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleDifferentialOilChangeMilageService
    {
        Task<VehicleResponseViewModel> SaveVehicleDifferentialOilChangeMilage(VehicleDifferentialOilChangeMilageViewModel vm,string userName);

        Task<VehicleResponseViewModel> DeleteVehicleDifferentialOilChangeMilage(long id, string userName);
        List<VehicleDifferentialOilChangeMilageViewModel> GetAllVehicleDifferentialOilChangeMilage(int vehicleId);
        VehicleDifferentialOilChangeMilageViewModel GetVehicleDifferentialOilChangeMilageById(long id);

        VehicleDifferentialOilChangeMilageViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
