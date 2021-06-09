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
        Task<VehicleResponseViewModel> SaveVehicleInsurance(VehicleInsuranceViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleInsurance(VehicleInsuranceViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleInsurance(long id, string userName);
        List<VehicleInsuranceViewModel> GetAllVehicleInsurance(int vehicleId);
        VehicleInsuranceViewModel GetVehicleInsuranceById(long id);
        VehicleInsuranceViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
