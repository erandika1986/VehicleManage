using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business
{
  public class VehicleInsuranceService : IVehicleInsuranceService
  {
    private readonly IVMDBUow _uow;
    private readonly IUserService _userService;
    private readonly IConfiguration config;
    private readonly ILogger<VehicleInsuranceService> logger;

    public VehicleInsuranceService(IVMDBUow uow, IUserService userService, IConfiguration config, ILogger<VehicleInsuranceService> logger)
    {
      this._uow = uow;
      this._userService = userService;
      this.config = config;
      this.logger = logger;
    }

    public async Task<VehicleResponseViewModel> AddNewVehicleInsurance(VehicleInsuranceViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);
        VehicleInsurance lastRecord = null;

        if (vm.ActualInsuranceDate.HasValue)
        {
          lastRecord = _uow.VehicleInsurance.GetAll().FirstOrDefault(t => t.VehicleId == vm.VehicleId && t.IsActive == true && t.ActualInsuranceDate.HasValue == false);
          lastRecord.ActualInsuranceDate = vm.ActualInsuranceDate.Value;
          lastRecord.UpdatedOn = DateTime.UtcNow;
          lastRecord.UpdatedBy = user.Id;

          _uow.VehicleInsurance.Update(lastRecord);
          await _uow.CommitAsync();
        }
        var model = new VehicleInsurance()
        {
          IsActive = true,
          VehicleId = vm.VehicleId,
          NextInsuranceDate = vm.NextInsuranceDate,
          ParentId = lastRecord == null ? (long?)null : lastRecord.Id,
          CreatedBy = user.Id,
          CreatedOn = DateTime.UtcNow,
          UpdatedBy = user.Id,
          UpdatedOn = DateTime.UtcNow
        };

        _uow.VehicleInsurance.Add(model);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "New Record has been added.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }

    public async Task<VehicleResponseViewModel> DeleteVehicleInsurance(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _uow.VehicleInsurance.GetAll().FirstOrDefault(t => t.Id == id);
        if (vt.ParentId.HasValue)
        {
          vt.Parent.ActualInsuranceDate = (DateTime?)null;
          vt.Parent.UpdatedBy = user.Id;
          vt.Parent.UpdatedOn = DateTime.UtcNow;
        }
        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _uow.VehicleInsurance.Update(vt);
        await _uow.CommitAsync();

        response.IsSuccess = true;
        response.Message = "Record has been deleted.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }

    public List<VehicleInsuranceViewModel> GetAllVehicleInsurance(int vehicleId)
    {
      var query = _uow.VehicleInsurance.GetAll().Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);
      var data = new List<VehicleInsuranceViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm());
      });


      return data;
    }

    public VehicleInsuranceViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _uow.VehicleInsurance.GetAll().Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm();
      }
      else
      {
        return new VehicleInsuranceViewModel();
      }
    }

    public VehicleInsuranceViewModel GetVehicleInsuranceById(long id)
    {
      var vtvm = _uow.VehicleInsurance.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

      return vtvm;
    }

  }
}

