using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleRevenueLicenceService
    {
        Task<VehicleResponseViewModel> AddNewVehicleRevenueLicence(VehicleRevenueLicenceViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleRevenueLicence(VehicleRevenueLicenceViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleRevenueLicence(long id, string userName);
        PaginatedItemsViewModel<VehicleRevenueLicenceViewModel> GetAllVehicleRevenueLicence(int vehicleId, int pageSize, int currentPage);
        VehicleRevenueLicenceViewModel GetVehicleRevenueLicenceById(long id);
        VehicleRevenueLicenceViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
