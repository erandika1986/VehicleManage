using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleGearBoxOilMilageService
    {
        Task<VehicleResponseViewModel> AddNewVehicleGearBoxOilMilage(VehicleGearBoxOilMilageViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleGearBoxOilMilage(VehicleGearBoxOilMilageViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleGearBoxOilMilage(long id, string userName);
        PaginatedItemsViewModel<VehicleGearBoxOilMilageViewModel> GetAllVehicleGearBoxOilMilage(int vehicleId, int pageSize, int currentPage);
        VehicleGearBoxOilMilageViewModel GetVehicleGearBoxOilMilageById(long id);
        VehicleGearBoxOilMilageViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
