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
        Task<VehicleResponseViewModel> SaveVehicleGreeceNiple(VehicleGreeceNipleViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleGreeceNiple(long id, string userName);
        List<VehicleGreeceNipleViewModel> GetAllVehicleGreeceNiple(int vehicleId);
        VehicleGreeceNipleViewModel GetVehicleGreeceNipleById(long id);
        VehicleGreeceNipleViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
