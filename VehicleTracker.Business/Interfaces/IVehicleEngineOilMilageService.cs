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
        Task<VehicleResponseViewModel> AddNewVehicleEngineOilMilage(VehicleEngineOilMilageViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleEngineOilMilage(VehicleEngineOilMilageViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleEngineOilMilage(long id, string userName);
        PaginatedItemsViewModel<VehicleEngineOilMilageViewModel> GetAllVehicleEngineOilMilage(int vehicleId, int pageSize, int currentPage);
        VehicleEngineOilMilageViewModel GetVehicleEngineOilMilageById(long id);
        VehicleEngineOilMilageViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
