using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleTypeService
    {
        VehicleTypeMasterDataViewModel GetVehicleTypeMasterData();
        Task<ResponseViewModel> SaveVehicleType(VehicleTypeViewModel vm);
        Task<ResponseViewModel> DeleteVehicleType(long id);
        List<VehicleTypeViewModel> GetAllVehicleTypes();
        VehicleTypeViewModel GetVehicleTypeById(long id);
    }
}
