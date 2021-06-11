using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleEngineOilMilageService
    {
        Task<VehicleResponseViewModel> SaveVehicleEngineOilMilage(VehicleEngineOilMilageViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleEngineOilMilage(long id, string userName);
        List<VehicleEngineOilMilageViewModel> GetAllVehicleEngineOilMilage(int vehicleId);
        VehicleEngineOilMilageViewModel GetVehicleEngineOilMilageById(long id);
        VehicleEngineOilMilageViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
