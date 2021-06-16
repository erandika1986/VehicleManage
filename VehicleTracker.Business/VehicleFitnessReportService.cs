using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
  public class VehicleFitnessReportService : IVehicleFitnessReportService
  {
    readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly IConfiguration config;
    private readonly ILogger<IVehicleFitnessReportService> logger;

    public VehicleFitnessReportService(VMDBContext db, IUserService userService, IConfiguration config, ILogger<IVehicleFitnessReportService> logger)
    {
      this._db = db;
      this._userService = userService;
      this.config = config;
      this.logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleFitnessReport(VehicleFitnessReportViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);
        var model = _db.VehicleFitnessReports.FirstOrDefault(x => x.Id == vm.Id);
        if (model == null)
        {
          model = vm.ToModel();
          model.CreatedBy = user.Id;
          model.UpdatedBy = user.Id;
          _db.VehicleFitnessReports.Add(model);
          await _db.SaveChangesAsync();


          response.Message = "New Record has been added.";
        }
        else
        {
          model.FitnessReportDate = new DateTime(vm.FitnessReportYear, vm.FitnessReportMonth, vm.FitnessReportDay, 0, 0, 0);
          model.ValidTill = new DateTime(vm.ValidTillYear, vm.ValidTillMonth, vm.ValidTillDay, 0, 0, 0);
          model.UpdatedBy = user.Id;
          model.UpdatedOn = DateTime.UtcNow;
          _db.VehicleFitnessReports.Update(model);
          await _db.SaveChangesAsync();

          response.Message = "Record has been updated.";
        }
        response.IsSuccess = true;
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Operation failed.Please try again.";
      }


      return response;
    }

    public async Task<VehicleResponseViewModel> DeleteVehicleFitnessReport(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _db.VehicleFitnessReports.FirstOrDefault(t => t.Id == id);


        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleFitnessReports.Update(vt);
        await _db.SaveChangesAsync();

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

    public List<VehicleFitnessReportViewModel> GetAllVehicleFitnessReport(int vehicleId)
    {
      var query = _db.VehicleFitnessReports.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.FitnessReportDate);

      var data = new List<VehicleFitnessReportViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm(config));
      });

      return data;
    }

    public VehicleFitnessReportViewModel GetVehicleFitnessReportById(long id)
    {
      var vtvm = _db.VehicleFitnessReports.FirstOrDefault(t => t.Id == id).ToVm(config);

      return vtvm;
    }

    public VehicleFitnessReportViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleFitnessReports.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm(config);
      }
      else
      {
        return new VehicleFitnessReportViewModel();
      }

    }

    public async Task<ResponseViewModel> UploadFitnessReportImage(FileContainerModel container, string userName)
    {
      var response = new ResponseViewModel();


      try
      {
        var user = _db.Users.FirstOrDefault(t => t.Username == userName);

        var fitnessReportRecord = _db.VehicleFitnessReports.FirstOrDefault(x => x.Id == container.Id);

        var folderPath = fitnessReportRecord.GetVehicleFitnessReportFolderPath(config);

        if (!string.IsNullOrEmpty(fitnessReportRecord.Attachment))
        {
          var existingImagePath = string.Format(@"{0}\{1}", folderPath, fitnessReportRecord.Attachment);
          if (File.Exists(existingImagePath))
          {
            File.Delete(existingImagePath);
          }
        }

        if (!Directory.Exists(folderPath))
        {
          Directory.CreateDirectory(folderPath);
        }

        var firstFile = container.Files.FirstOrDefault();
        if (firstFile != null && firstFile.Length > 0)
        {
          var fileName = fitnessReportRecord.GetVehicleFitnessReportImageName(Path.GetExtension(firstFile.FileName));
          var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await firstFile.CopyToAsync(stream);

            fitnessReportRecord.Attachment = fileName;

            _db.VehicleFitnessReports.Update(fitnessReportRecord);

            await _db.SaveChangesAsync();

          }
        }

        response.IsSuccess = true;
        response.Message = "Fitness Report image has been uploaded succesfully.";
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while uploading the file. Please try again.";
      }

      return response;
    }

    public DownloadFileViewModel DownloadFitnessReportImage(int id)
    {
      var response = new DownloadFileViewModel();
      try
      {
        var fitnessReportRecord = _db.VehicleFitnessReports.FirstOrDefault(t => t.Id == id);
        var imagePath = fitnessReportRecord.GetVehicleFitnessReportImagePath(config);
        byte[] fileContents = null;
        MemoryStream ms = new MemoryStream();

        using (FileStream fs = File.OpenRead(imagePath))
        {
          fs.CopyTo(ms);
          fileContents = ms.ToArray();
          ms.Dispose();
          response.FileData = fileContents;
        }

        response.FileName = fitnessReportRecord.Attachment;
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
      }

      return response;
    }
  }
}
