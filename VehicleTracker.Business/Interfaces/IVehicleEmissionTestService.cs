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
        Task<VehicleResponseViewModel> SaveVehicleEmissionTest(VehicleEmissionTestViewModel vm, string userName);
        //Task<VehicleResponseViewModel> UpdateVehicleEmissionTest(VehicleEmissionTestViewModel vm, string userName);
        Task<VehicleResponseViewModel> DeleteVehicleEmissionTest(long id, string userName);
        List<VehicleEmissionTestViewModel> GetAllVehicleEmissionTest(int vehicleId);
        VehicleEmissionTestViewModel GetVehicleEmissionTestById(long id);
        VehicleEmissionTestViewModel GetLatestRecordForVehicle(long vehicleId);
        Task<ResponseViewModel> UploadEmissionTestImage(FileContainerModel container, string userName);
        DownloadFileViewModel DownloadEmissionTestImage(int id);
  }
}
