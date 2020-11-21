using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.VehicleBeat;

namespace VehicleTracker.Business.Interfaces
{
    public interface IVehicleBeatService
    {
        Task<ResponseViewModel> AddNewVehicleBeatRecord(DailyVehicleBeatViewModel vm, string userName);
        Task<ResponseViewModel> UpdateNewVehicleBeatRecord(DailyVehicleBeatViewModel vm, string userName);
        DailyVehicleBeatViewModel GetVehicleBeatRecordById(long id);
        Task<ResponseViewModel> DeleteSelectedBeatRecord(long id, string userName);
        List<DailyVehicleBeatViewModel> GetAllVehicleBeatRecord(VehicleBeatFilterViewModel filters);
        VehicleBeatMasterDataViewModel GetMasterData();

    }
}
