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
        Task<VehicleResponseViewModel> SaveVehicleFuelFilterMilage(VehicleFuelFilterMilageViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleFuelFilterMilage(long id, string userName);
        List<VehicleFuelFilterMilageViewModel> GetAllVehicleFuelFilterMilage(int vehicleId);
        VehicleFuelFilterMilageViewModel GetVehicleFuelFilterMilageById(long id);
        VehicleFuelFilterMilageViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
