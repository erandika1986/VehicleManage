using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleFuelFilterMilageService
    {
        Task<VehicleResponseViewModel> AddNewVehicleFuelFilterMilage(VehicleFuelFilterMilageViewModel vm, string userName);
        ///Task<VehicleResponseViewModel> UpdateVehicleFuelFilterMilage(VehicleFuelFilterMilageViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleFuelFilterMilage(long id, string userName);
        PaginatedItemsViewModel<VehicleFuelFilterMilageViewModel> GetAllVehicleFuelFilterMilage(int vehicleId, int pageSize, int currentPage);
        VehicleFuelFilterMilageViewModel GetVehicleFuelFilterMilageById(long id);
        VehicleFuelFilterMilageViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
