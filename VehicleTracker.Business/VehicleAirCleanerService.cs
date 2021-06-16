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
  public class VehicleAirCleanerService : IVehicleAirCleanerService
  {
    private readonly ILogger<IVehicleAirCleanerService> _logger;
    readonly VMDBContext _db;
    private readonly IUserService _userService;

    public VehicleAirCleanerService(VMDBContext db, IUserService userService, ILogger<IVehicleAirCleanerService> logger)
    {
      this._db = db;
      this._userService = userService;
      this._logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleAirCleaner(VehicleAirCleanerViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);
        var model = _db.VehicleAirCleaners.FirstOrDefault(x => x.Id == vm.Id);
        if(model==null)
        {
          model = vm.ToModel();
          model.CreatedBy = user.Id;
          model.UpdatedBy = user.Id;
          _db.VehicleAirCleaners.Add(model);
          response.Message = "New Record has been added.";
        }
        else
        {
          model.NextAirCleanerReplaceMilage = vm.NextAirCleanerReplaceMilage;
          model.AirCleanerReplaceMilage = vm.AirCleanerReplaceMilage;
          model.UpdatedBy = user.Id;
          model.UpdatedOn = DateTime.UtcNow;
          model.Note = vm.Note;
          _db.VehicleAirCleaners.Update(model);
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

    public async Task<VehicleResponseViewModel> DeleteVehicleAirCleaner(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _db.VehicleAirCleaners.FirstOrDefault(t => t.Id == id);
        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleAirCleaners.Update(vt);
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

    public List<VehicleAirCleanerViewModel> GetAllVehicleAirCleaner(int vehicleId)
    {
      var query = _db.VehicleAirCleaners.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

      var data = new List<VehicleAirCleanerViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm());
      });


      return data;
    }

    public VehicleAirCleanerViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleAirCleaners.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm();
      }
      else
      {
        return new VehicleAirCleanerViewModel();
      }
    }

    public VehicleAirCleanerViewModel GetVehicleAirCleanerById(long id)
    {
      var vtvm = _db.VehicleAirCleaners.FirstOrDefault(t => t.Id == id).ToVm();

      return vtvm;
    }


  }
}
