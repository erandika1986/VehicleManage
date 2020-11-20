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
        Task<VehicleResponseViewModel> AddNewVehicleAirCleaner(VehicleAirCleanerViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleAirCleaner(VehicleAirCleanerViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleAirCleaner(long id, string userName);
        PaginatedItemsViewModel<VehicleAirCleanerViewModel> GetAllVehicleAirCleaner(int vehicleId, int pageSize, int currentPage);
        VehicleAirCleanerViewModel GetVehicleAirCleanerById(long id);
        VehicleAirCleanerViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
