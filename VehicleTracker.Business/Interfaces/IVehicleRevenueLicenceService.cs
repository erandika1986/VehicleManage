using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business.Interfaces
{
  public interface IVehicleRevenueLicenceService
  {
    Task<VehicleResponseViewModel> SaveVehicleRevenueLicence(VehicleRevenueLicenceViewModel vm, string userName);
    //Task<VehicleResponseViewModel> UpdateVehicleRevenueLicence(VehicleRevenueLicenceViewModel vm, string userName);
    Task<VehicleResponseViewModel> DeleteVehicleRevenueLicence(long id, string userName);
    List<VehicleRevenueLicenceViewModel> GetAllVehicleRevenueLicence(int vehicleId);
    VehicleRevenueLicenceViewModel GetVehicleRevenueLicenceById(long id);
    VehicleRevenueLicenceViewModel GetLatestRecordForVehicle(long vehicleId);
    DownloadFileViewModel DownloadInsuranceImage(int id);
    Task<ResponseViewModel> UploadRevenueLicenceImage(FileContainerModel container, string userName);
  }
}
