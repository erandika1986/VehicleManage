using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business
{
    public interface IVehicleService
    {
        VehicleTypeMasterDataViewModel GetVehicleTypeMasterData();
        Task<ResponseViewModel> AddNewVehicleType(VehicleTypeViewModel vm);
        Task<ResponseViewModel> UpdateVehicleType(VehicleTypeViewModel vm);
        Task<ResponseViewModel> DeleteVehicleType(long id);
        PaginatedItemsViewModel<VehicleTypeViewModel> GetAllVehicleTypes(int pageSize, int currentPage);
        VehicleTypeViewModel GetVehicleTypeById(long id);

        VehicleMasterDataViewModel GetVehicleMasterData();
        Task<VehicleResponseViewModel> AddNewVehicle(VehicleViewModel vm,string userName);
        Task<ResponseViewModel> UpdateVehicle(VehicleViewModel vm, string userName);
        Task<ResponseViewModel> DeleteVehicle(long id);
        PaginatedItemsViewModel<VehicleViewModel> GetAllVehicles(int pageSize, int currentPage,string searchText);
        VehicleViewModel GetVehicleById(long id);

        Task<ResponseViewModel> AddNewVehicleAirCleanerRecord(VehicleAirCleanerViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleDifferentialOilChangeMilageRecord(VehicleDifferentialOilChangeMilageViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleEmissionTestRecord(VehicleEmissionTestViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleEngineOilMilageRecord(VehicleEngineOilMilageViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleExpenseRecord(VehicleExpenseViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleFitnessReportRecord(VehicleFitnessReportViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleFuelFilterMilageRecord(VehicleFuelFilterMilageViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleGearBoxOilMilageRecord(VehicleGearBoxOilMilageViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleGreeceNipleRecord(VehicleGreeceNipleViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleInsuranceRecord(VehicleInsuranceViewModel vm, string userName);

        Task<ResponseViewModel> AddNewVehicleRevenueLicenceRecord(VehicleRevenueLicenceViewModel vm, string userName);
        ResponseViewModel IsVehicleAlreadyExists(string regNo);


    }
}
