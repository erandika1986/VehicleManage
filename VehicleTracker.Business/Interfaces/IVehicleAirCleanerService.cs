using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleAirCleanerService
    {
        Task<VehicleResponseViewModel> SaveVehicleAirCleaner(VehicleAirCleanerViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleAirCleaner(long id, string userName);
        List<VehicleAirCleanerViewModel> GetAllVehicleAirCleaner(int vehicleId);
        VehicleAirCleanerViewModel GetVehicleAirCleanerById(long id);
        VehicleAirCleanerViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
