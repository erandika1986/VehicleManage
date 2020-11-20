using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleInsuranceService
    {
        Task<VehicleResponseViewModel> AddNewVehicleInsurance(VehicleInsuranceViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleInsurance(VehicleInsuranceViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleInsurance(long id, string userName);
        PaginatedItemsViewModel<VehicleInsuranceViewModel> GetAllVehicleInsurance(int vehicleId, int pageSize, int currentPage);
        VehicleInsuranceViewModel GetVehicleInsuranceById(long id);
        VehicleInsuranceViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
