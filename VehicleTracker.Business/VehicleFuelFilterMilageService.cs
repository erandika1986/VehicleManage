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
  public class VehicleFuelFilterMilageService : IVehicleFuelFilterMilageService
  {
    private readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly ILogger<IVehicleFuelFilterMilageService> _logger;

    public VehicleFuelFilterMilageService(VMDBContext db, IUserService userService, ILogger<IVehicleFuelFilterMilageService> logger)
    {
      this._db = db;
      this._userService = userService;
      this._logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleFuelFilterMilage(VehicleFuelFilterMilageViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var model = _db.VehicleFuelFilterMilages.FirstOrDefault(x => x.Id == vm.Id);

        if(model==null)
        {
          model = vm.ToModel();
          model.CreatedBy = user.Id;
          model.UpdatedBy = user.Id;
          _db.VehicleFuelFilterMilages.Add(model);

          response.Message = "New Record has been added.";
        }
        else
        {
          model.FuelFilterChangeMilage = vm.FuelFilterChangeMilage;
          model.NextFuelFilterChangeMilage = vm.NextFuelFilterChangeMilage;
          model.UpdatedBy = user.Id;
          model.UpdatedOn = DateTime.UtcNow;
          _db.VehicleFuelFilterMilages.Update(model);

          response.Message = "Record has been updated.";
        }

        await _db.SaveChangesAsync();
        response.IsSuccess = true;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }

    public async Task<VehicleResponseViewModel> DeleteVehicleFuelFilterMilage(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _db.VehicleFuelFilterMilages.FirstOrDefault(t => t.Id == id);

        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleFuelFilterMilages.Update(vt);
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

    public List<VehicleFuelFilterMilageViewModel> GetAllVehicleFuelFilterMilage(int vehicleId)
    {
      var query = _db.VehicleFuelFilterMilages.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

      var data = new List<VehicleFuelFilterMilageViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm());
      });


      return data;
    }

    public VehicleFuelFilterMilageViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleFuelFilterMilages.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm();
      }
      else
      {
        return new VehicleFuelFilterMilageViewModel();
      }
    }

    public VehicleFuelFilterMilageViewModel GetVehicleFuelFilterMilageById(long id)
    {
      var vtvm = _db.VehicleFuelFilterMilages.FirstOrDefault(t => t.Id == id).ToVm();

      return vtvm;
    }


  }
}
