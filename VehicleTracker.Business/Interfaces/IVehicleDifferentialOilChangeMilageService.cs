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
        Task<VehicleResponseViewModel> AddNewVehicleDifferentialOilChangeMilage(VehicleDifferentialOilChangeMilageViewModel vm,string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleDifferentialOilChangeMilage(VehicleDifferentialOilChangeMilageViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleDifferentialOilChangeMilage(long id, string userName);
        PaginatedItemsViewModel<VehicleDifferentialOilChangeMilageViewModel> GetAllVehicleDifferentialOilChangeMilage(int vehicleId, int pageSize, int currentPage);
        VehicleDifferentialOilChangeMilageViewModel GetVehicleDifferentialOilChangeMilageById(long id);

        VehicleDifferentialOilChangeMilageViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
