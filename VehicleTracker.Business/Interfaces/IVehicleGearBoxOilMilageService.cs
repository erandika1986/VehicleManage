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
        Task<VehicleResponseViewModel> SaveVehicleGearBoxOilMilage(VehicleGearBoxOilMilageViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleGearBoxOilMilage(long id, string userName);
        List<VehicleGearBoxOilMilageViewModel> GetAllVehicleGearBoxOilMilage(int vehicleId);
        VehicleGearBoxOilMilageViewModel GetVehicleGearBoxOilMilageById(long id);
        VehicleGearBoxOilMilageViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
