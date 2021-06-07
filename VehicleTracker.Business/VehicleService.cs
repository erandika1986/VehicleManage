using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common.Enums;
using VehicleTracker.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VehicleTracker.Business
{
  public class VehicleService : IVehicleService
  {
    private readonly IVMDBUow _uow;
    private readonly IUserService _userService;
    private readonly IConfiguration config;
    private readonly ILogger<VehicleService> logger;

    public VehicleService(IVMDBUow uow, IUserService userService, IConfiguration config, ILogger<VehicleService> logger)
    {
      this._uow = uow;
      this._userService = userService;
      this.config = config;
      this.logger = logger;
    }




    public VehicleMasterDataViewModel GetVehicleMasterData()
    {
      var masterData = new VehicleMasterDataViewModel();

      var vehicleTypes = _uow.VehicleType.GetAll().ToList();

      vehicleTypes.ForEach(item =>
      {
        masterData.VehicleTypes.Add(new DropDownViewModal() { Id = (int)item.Id, Name = item.Name });
      });

      int currentYear = DateTime.Now.Year;

      for (int i = currentYear; i >= currentYear - 30; i--)
      {
        masterData.ProductionYears.Add(new DropDownViewModal() { Id = i, Name = i.ToString() });
      }

      return masterData;
    }
    public async Task<VehicleResponseViewModel> SaveVehicle(VehicleViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);
        var vehicle = _uow.Vehicle.GetAll().FirstOrDefault(t => t.Id == vm.Id);
        if(vehicle==null)
        {
          if (IsVehicleAlreadyExists(vm.RegistrationNo).IsSuccess)
          {
            response.IsSuccess = false;
            response.Message = "Vehcile already registered with system.";

            return response;
          }


          var vt = vm.ToModel();
          vt.UpdatedBy = user.Id;
          vt.CreatedBy = user.Id;
          vt.CreatedOn = DateTime.UtcNow;
          vt.UpdatedOn = DateTime.UtcNow;

          _uow.Vehicle.Add(vt);
          await _uow.CommitAsync();
          response.Id = vt.Id;

          response.IsSuccess = true;
          response.Message = "New Vehicle has been added.";
        }
        else
        {
          vehicle.UpdatedOn = DateTime.UtcNow;
          vehicle.UpdatedBy = user.Id;
          vehicle.ProductionYear = vm.ProductionYear;
          vehicle.InitialOdometerReading = vm.InitialOdometerReading;
          vehicle.IsActive = vm.IsActive;


          _uow.Vehicle.Update(vehicle);
          await _uow.CommitAsync();

          response.IsSuccess = true;
          response.Message = "Vehicle detail has been updated.";
        }
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }

    public async Task<ResponseViewModel> DeleteVehicle(long id)
    {
      var response = new ResponseViewModel();
      try
      {
        var vehicle = _uow.Vehicle.GetAll().FirstOrDefault(t => t.Id == id);
        vehicle.IsActive = false;
        _uow.Vehicle.Update(vehicle);

        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "Vehicle has been deleted.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public PaginatedItemsViewModel<VehicleViewModel> GetAllVehicles(int pageSize, int currentPage, string sortBy, string sortDirection, string searchText)
    {
      var query = _uow.Vehicle.GetAll().Where(t => t.IsActive == true);

      if (!string.IsNullOrEmpty(searchText))
      {
        query = _uow.Vehicle.GetAll().Where(t => t.RegistrationNo.Contains(searchText));
      }

      switch (sortBy)
      {
        case "registrationNo":
          {
            if (sortDirection == "desc")
            {
              query = query.OrderByDescending(x => x.RegistrationNo);
            }
            else
            {
              query = query.OrderBy(x => x.RegistrationNo);
            }

          }
          break;
        case "vehicelTypeName":
          {
            if (sortDirection == "desc")
            {
              query = query.OrderBy(x => x.VehicelTypeId);
            }
            else
            {
              query = query.OrderBy(x => x.VehicelTypeId);
            }

          }
          break;
        case "initialOdometerReading":
          {
            if (sortDirection == "desc")
            {
              query = query.OrderByDescending(x => x.InitialOdometerReading);
            }
            else
            {
              query = query.OrderBy(x => x.InitialOdometerReading);
            }

          }
          break;
        case "productionYear":
          {
            if (sortDirection == "desc")
            {
              query = query.OrderByDescending(x => x.ProductionYear);
            }
            else
            {
              query = query.OrderBy(x => x.ProductionYear);
            }

          }
          break;
        case "isActive":
          {
            if (sortDirection == "desc")
            {
              query = query.OrderByDescending(x => x.IsActive);
            }
            else
            {
              query = query.OrderBy(x => x.IsActive);
            }

          }
          break;
      }



      int totalRecordCount = 0;
      double totalPages = 0;
      int totalPageCount = 0;
      var data = new List<VehicleViewModel>();

      totalRecordCount = query.Count();
      totalPages = (double)totalRecordCount / pageSize;
      totalPageCount = (int)Math.Ceiling(totalPages);

      var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm());
      });

      var response = new PaginatedItemsViewModel<VehicleViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


      return response;

    }
    public VehicleViewModel GetVehicleById(long id)
    {
      var vehicle = _uow.Vehicle.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

      return vehicle;
    }

    public async Task<ResponseViewModel> AddNewVehicleAirCleanerRecord(VehicleAirCleanerViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vac = vm.ToModel();
        vac.UpdatedBy = user.Id;
        vac.CreatedBy = user.Id;

        _uow.VehicleAirCleaner.Add(vac);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Air Cleaner Record has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleDifferentialOilChangeMilageRecord(VehicleDifferentialOilChangeMilageViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vac = vm.ToModel();
        vac.UpdatedBy = user.Id;
        vac.CreatedBy = user.Id;

        _uow.VehicleDifferentialOilChangeMilage.Add(vac);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Differential Oil Change Milage Record has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleEmissionTestRecord(VehicleEmissionTestViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vet = vm.ToModel();
        vet.UpdatedBy = user.Id;
        vet.CreatedBy = user.Id;

        _uow.VehicleEmissiontTest.Add(vet);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Emission Test record has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleEngineOilMilageRecord(VehicleEngineOilMilageViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var veom = vm.ToModel();
        veom.UpdatedBy = user.Id;
        veom.CreatedBy = user.Id;

        _uow.VehicleEngineOilMilage.Add(veom);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Engine Oil Milage has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleExpenseRecord(VehicleExpenseViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var veom = vm.ToModel();
        veom.UpdatedBy = user.Id;
        veom.CreatedBy = user.Id;

        _uow.VehicleExpense.Add(veom);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Expense has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleFitnessReportRecord(VehicleFitnessReportViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vfr = vm.ToModel();
        vfr.UpdatedBy = user.Id;
        vfr.CreatedBy = user.Id;

        _uow.VehicleFitnessReport.Add(vfr);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Fitness Report has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleFuelFilterMilageRecord(VehicleFuelFilterMilageViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vffm = vm.ToModel();
        vffm.UpdatedBy = user.Id;
        vffm.CreatedBy = user.Id;

        _uow.VehicleFuelFilterMilage.Add(vffm);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Fuel Filter Milage has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleGearBoxOilMilageRecord(VehicleGearBoxOilMilageViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vgbom = vm.ToModel();
        vgbom.UpdatedBy = user.Id;
        vgbom.CreatedBy = user.Id;

        _uow.VehicleGearBoxOilMilage.Add(vgbom);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Gear Box Oil Milage has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleGreeceNipleRecord(VehicleGreeceNipleViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vgn = vm.ToModel();
        vgn.UpdatedBy = user.Id;
        vgn.CreatedBy = user.Id;

        _uow.VehicleGreeceNiple.Add(vgn);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Greece Niple record has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleInsuranceRecord(VehicleInsuranceViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vi = vm.ToModel();
        vi.UpdatedBy = user.Id;
        vi.CreatedBy = user.Id;

        _uow.VehicleInsurance.Add(vi);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle nsurance record has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }
    public async Task<ResponseViewModel> AddNewVehicleRevenueLicenceRecord(VehicleRevenueLicenceViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vi = vm.ToModel();
        vi.UpdatedBy = user.Id;
        vi.CreatedBy = user.Id;

        _uow.VehicleRevenueLicence.Add(vi);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Vehicle Revenue Licence has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }

    public ResponseViewModel IsVehicleAlreadyExists(string regNo)
    {
      var response = new ResponseViewModel();
      var regNoInLower = regNo.Replace(" ", string.Empty).ToLower().Trim();

      var vehicle = _uow.Vehicle.GetAll().FirstOrDefault(t => t.RegistrationNo.Replace(" ", "").ToLower().Trim() == regNoInLower);
      if (vehicle == null)
      {
        response.IsSuccess = false;
        response.Message = "Vehcile not exists";
      }
      else
      {
        response.IsSuccess = true;
        response.Message = "Vehcile already registered with system.";
      }

      return response;
    }

  }
}
