using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleFitnessReportService
    {
        Task<VehicleResponseViewModel> AddNewVehicleFitnessReport(VehicleFitnessReportViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleFitnessReport(VehicleFitnessReportViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleFitnessReport(long id, string userName);
        PaginatedItemsViewModel<VehicleFitnessReportViewModel> GetAllVehicleFitnessReport(int vehicleId, int pageSize, int currentPage);
        VehicleFitnessReportViewModel GetVehicleFitnessReportById(long id);
        VehicleFitnessReportViewModel GetLatestRecordForVehicle(long vehicleId);
    }
}
