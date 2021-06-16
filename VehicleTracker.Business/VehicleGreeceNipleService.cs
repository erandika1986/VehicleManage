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
  public class VehicleGreeceNipleService : IVehicleGreeceNipleService
  {
    private readonly VMDBContext _db;
    private readonly ILogger<IVehicleGreeceNipleService> _logger;
    private readonly IUserService _userService;
 

    public VehicleGreeceNipleService(VMDBContext db, IUserService userService, ILogger<IVehicleGreeceNipleService> logger)
    {
      this._db = db;
      this._userService = userService;
      this._logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleGreeceNiple(VehicleGreeceNipleViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var model = _db.VehicleGreeceNiples.FirstOrDefault(x => x.Id == vm.Id);

        if(model==null)
        {
          model = vm.ToModel();
          model.CreatedBy = user.Id;
          model.UpdatedBy = user.Id;

          _db.VehicleGreeceNiples.Add(model);
          response.Message = "New Record has been added.";

        }
        else
        {
          model.GreeceNipleReplaceDate = new DateTime(vm.GreeceNipleReplacYear, vm.GreeceNipleReplacMonth, vm.GreeceNipleReplacDay, 0, 0, 0);
          model.NextGreeceNipleReplaceDate = new DateTime(vm.NextGreeceNipleReplaceYear, vm.NextGreeceNipleReplaceMonth, vm.NextGreeceNipleReplaceDay, 0, 0, 0);
          model.UpdatedBy = user.Id;
          model.UpdatedOn = DateTime.UtcNow;
          model.Note = vm.Note;

          _db.VehicleGreeceNiples.Update(model);
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

    public async Task<VehicleResponseViewModel> DeleteVehicleGreeceNiple(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _db.VehicleGreeceNiples.FirstOrDefault(t => t.Id == id);

        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleGreeceNiples.Update(vt);
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

    public List<VehicleGreeceNipleViewModel> GetAllVehicleGreeceNiple(int vehicleId)
    {
      var query = _db.VehicleGreeceNiples.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

      var data = new List<VehicleGreeceNipleViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm());
      });


      return data;
    }

    public VehicleGreeceNipleViewModel GetVehicleGreeceNipleById(long id)
    {
      var vtvm = _db.VehicleGreeceNiples.FirstOrDefault(t => t.Id == id).ToVm();

      return vtvm;
    }


    public VehicleGreeceNipleViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleGreeceNiples.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm();
      }
      else
      {
        return new VehicleGreeceNipleViewModel();
      }

    }
  }
}
