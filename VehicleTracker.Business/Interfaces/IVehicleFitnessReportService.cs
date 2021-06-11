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
    Task<VehicleResponseViewModel> SaveVehicleFitnessReport(VehicleFitnessReportViewModel vm, string userName);
    //Task<VehicleResponseViewModel> UpdateVehicleFitnessReport(VehicleFitnessReportViewModel vm, string userName);
    Task<VehicleResponseViewModel> DeleteVehicleFitnessReport(long id, string userName);
    List<VehicleFitnessReportViewModel> GetAllVehicleFitnessReport(int vehicleId);
    VehicleFitnessReportViewModel GetVehicleFitnessReportById(long id);
    VehicleFitnessReportViewModel GetLatestRecordForVehicle(long vehicleId);
    Task<ResponseViewModel> UploadFitnessReportImage(FileContainerModel container, string userName);
    DownloadFileViewModel DownloadFitnessReportImage(int id);
  }
}
