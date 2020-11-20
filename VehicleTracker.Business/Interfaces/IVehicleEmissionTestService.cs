using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleEmissionTestService
    {
        Task<VehicleResponseViewModel> AddNewVehicleEmissionTest(VehicleEmissionTestViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleEmissionTest(VehicleEmissionTestViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleEmissionTest(long id, string userName);
        PaginatedItemsViewModel<VehicleEmissionTestViewModel> GetAllVehicleEmissionTest(int vehicleId, int pageSize, int currentPage);
        VehicleEmissionTestViewModel GetVehicleEmissionTestById(long id);
        VehicleEmissionTestViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
