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
  public class VehicleGearBoxOilMilageService : IVehicleGearBoxOilMilageService
  {
    private readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly ILogger<IVehicleGearBoxOilMilageService> _logger;

    public VehicleGearBoxOilMilageService(VMDBContext db, IUserService userService, ILogger<IVehicleGearBoxOilMilageService> logger)
    {
      this._db = db;
      this._userService = userService;
      this._logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleGearBoxOilMilage(VehicleGearBoxOilMilageViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var model = _db.VehicleGearBoxOilMilages.FirstOrDefault(x => x.Id == vm.Id);

        if(model==null)
        {
          model = vm.ToModel();
          model.CreatedBy = user.Id;
          model.UpdatedBy = user.Id;

          _db.VehicleGearBoxOilMilages.Add(model);
          response.Message = "New Record has been added.";
        }
        else
        {
          model.GearBoxOilChangeMilage = vm.GearBoxOilChangeMilage;
          model.NextGearBoxOilChangeMilage = vm.NextGearBoxOilChangeMilage;
          model.UpdatedBy = user.Id;
          model.UpdatedOn = DateTime.UtcNow;
          model.Note = vm.Note;

          _db.VehicleGearBoxOilMilages.Update(model);
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

    public async Task<VehicleResponseViewModel> DeleteVehicleGearBoxOilMilage(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _db.VehicleGearBoxOilMilages.FirstOrDefault(t => t.Id == id);
        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleGearBoxOilMilages.Update(vt);
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

    public List<VehicleGearBoxOilMilageViewModel> GetAllVehicleGearBoxOilMilage(int vehicleId)
    {
      var query = _db.VehicleGearBoxOilMilages.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

      var data = new List<VehicleGearBoxOilMilageViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm());
      });


      return data;
    }

    public VehicleGearBoxOilMilageViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleGearBoxOilMilages.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm();
      }
      else
      {
        return new VehicleGearBoxOilMilageViewModel();
      }
    }

    public VehicleGearBoxOilMilageViewModel GetVehicleGearBoxOilMilageById(long id)
    {
      var vtvm = _db.VehicleGearBoxOilMilages.FirstOrDefault(t => t.Id == id).ToVm();

      return vtvm;
    }


  }
}
