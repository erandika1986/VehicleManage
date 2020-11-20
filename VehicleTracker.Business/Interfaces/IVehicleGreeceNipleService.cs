using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleGreeceNipleService
    {
        Task<VehicleResponseViewModel> AddNewVehicleGreeceNiple(VehicleGreeceNipleViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleGreeceNiple(VehicleGreeceNipleViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleGreeceNiple(long id, string userName);
        PaginatedItemsViewModel<VehicleGreeceNipleViewModel> GetAllVehicleGreeceNiple(int vehicleId, int pageSize, int currentPage);
        VehicleGreeceNipleViewModel GetVehicleGreeceNipleById(long id);
        VehicleGreeceNipleViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
