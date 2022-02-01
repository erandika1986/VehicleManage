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
        Task<ResponseViewModel> SaveDailyVehicleBeatRecord(DailyVehicleBeatViewModel vm, string userName);
        DailyVehicleBeatViewModel GetVehicleBeatRecordById(long id);
        Task<ResponseViewModel> DeleteSelectedBeatRecord(long id, string userName);
        Task<ResponseViewModel> AddSalesOrdersToDailyBeat(DailyBeatSalesOrderViewModel vm, string userName);
        Task<ResponseViewModel> DeleteSalesOrderFromDailyBeat(int id);
        PaginatedItemsViewModel<DailyVehicleBeatViewModel> GetAllVehicleBeatRecord(VehicleBeatFilterViewModel filters, string userName);
        VehicleBeatMasterDataViewModel GetMasterData();

    }
}
