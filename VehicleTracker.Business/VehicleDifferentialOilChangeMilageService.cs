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
  public class VehicleDifferentialOilChangeMilageService : IVehicleDifferentialOilChangeMilageService
  {
    private readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly ILogger<IVehicleDifferentialOilChangeMilageService> _logger;

    public VehicleDifferentialOilChangeMilageService(VMDBContext db, IUserService userService, ILogger<IVehicleDifferentialOilChangeMilageService> logger)
    {
      this._db = db;
      this._userService = userService;
      this._logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleDifferentialOilChangeMilage(VehicleDifferentialOilChangeMilageViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var model = _db.VehicleDifferentialOilChangeMilages.FirstOrDefault(x => x.Id == vm.Id);
        if(model==null)
        {
           model = vm.ToModel();
          model.CreatedBy = user.Id;
          model.UpdatedBy = user.Id;
          _db.VehicleDifferentialOilChangeMilages.Add(model);


          response.IsSuccess = true;
          response.Message = "New Record has been added.";
        }
        else
        {
          model.DifferentialOilChangeMilage = vm.DifferentialOilChangeMilage;
          model.NextDifferentialOilChangeMilage = vm.NextDifferentialOilChangeMilage;
          model.UpdatedOn = DateTime.UtcNow;
          model.UpdatedBy = user.Id;
          _db.VehicleDifferentialOilChangeMilages.Update(model);

          response.Message = "Record has been updated.";
        }
        await _db.SaveChangesAsync();

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }

    public async Task<VehicleResponseViewModel> DeleteVehicleDifferentialOilChangeMilage(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _db.VehicleDifferentialOilChangeMilages.FirstOrDefault(t => t.Id == id);
        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleDifferentialOilChangeMilages.Update(vt);
        await _db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Record has been deleted.";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }

    public List<VehicleDifferentialOilChangeMilageViewModel> GetAllVehicleDifferentialOilChangeMilage(int vehicleId)
    {
      var query = _db.VehicleDifferentialOilChangeMilages.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

      var data = new List<VehicleDifferentialOilChangeMilageViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm());
      });

      return data;
    }

    public VehicleDifferentialOilChangeMilageViewModel GetVehicleDifferentialOilChangeMilageById(long id)
    {
      var vtvm = _db.VehicleDifferentialOilChangeMilages.FirstOrDefault(t => t.Id == id).ToVm();

      return vtvm;
    }

    public VehicleDifferentialOilChangeMilageViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleDifferentialOilChangeMilages.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm();
      }
      else
      {
        return new VehicleDifferentialOilChangeMilageViewModel();
      }

    }


  }
}
