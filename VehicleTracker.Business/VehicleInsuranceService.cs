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

        var model = vm.ToModel();
        model.CreatedBy = user.Id;
        model.UpdatedBy = user.Id;

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

