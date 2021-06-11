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
  public class VehicleRevenueLicenceService : IVehicleRevenueLicenceService
  {

    private readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly IConfiguration config;
    private readonly ILogger<IVehicleRevenueLicenceService> logger;

    public VehicleRevenueLicenceService(VMDBContext db, IUserService userService, IConfiguration config, ILogger<IVehicleRevenueLicenceService> logger)
    {
      this._db = db;
      this._userService = userService;
      this.config = config;
      this.logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleRevenueLicence(VehicleRevenueLicenceViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();

      try
      {
        var user = _userService.GetUserByUsername(userName);

        var model = _db.VehicleRevenueLicence.FirstOrDefault(x => x.Id == vm.Id);

        if (model == null)
        {
          model = vm.ToModel();
          model.CreatedBy = user.Id;
          model.UpdatedBy = user.Id;

          _db.VehicleRevenueLicence.Add(model);
          await _db.SaveChangesAsync();

          response.Message = "New record has been added.";
        }
        else
        {
          model.RevenueLicenceDate = new DateTime(vm.RevenueLicenceYear, vm.RevenueLicenceMonth, vm.RevenueLicenceDay, 0, 0, 0);
          model.ValidTill = new DateTime(vm.ValidTillYear, vm.ValidTillMonth, vm.ValidTillDay, 0, 0, 0);
          model.UpdatedBy = user.Id;
          model.UpdatedOn = DateTime.UtcNow;

          _db.VehicleRevenueLicence.Update(model);
          response.Message = "Record has been updated.";
        }

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

    public async Task<VehicleResponseViewModel> DeleteVehicleRevenueLicence(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _db.VehicleRevenueLicence.FirstOrDefault(t => t.Id == id);
        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleRevenueLicence.Update(vt);
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

    public List<VehicleRevenueLicenceViewModel> GetAllVehicleRevenueLicence(int vehicleId)
    {
      var query = _db.VehicleRevenueLicence.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.RevenueLicenceDate);

      var data = new List<VehicleRevenueLicenceViewModel>();


      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm(config));
      });

      return data;
    }

    public VehicleRevenueLicenceViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleRevenueLicence.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm(config);
      }
      else
      {
        return new VehicleRevenueLicenceViewModel();
      }
    }

    public VehicleRevenueLicenceViewModel GetVehicleRevenueLicenceById(long id)
    {
      var vtvm = _db.VehicleRevenueLicence.FirstOrDefault(t => t.Id == id).ToVm(config);

      return vtvm;
    }

    public async Task<ResponseViewModel> UploadRevenueLicenceImage(FileContainerModel container, string userName)
    {
      var response = new ResponseViewModel();


      try
      {
        var user = _db.User.FirstOrDefault(t => t.Username == userName);

        var licenceRecord = _db.VehicleRevenueLicence.FirstOrDefault(x => x.Id == container.Id);

        var folderPath = licenceRecord.GetVehicleRevenueLicenceFolderPath(config);

        if (!string.IsNullOrEmpty(licenceRecord.Attachment))
        {
          var existingImagePath = string.Format(@"{0}\{1}", folderPath, licenceRecord.Attachment);

          File.Delete(existingImagePath);
        }

        if (!Directory.Exists(folderPath))
        {
          Directory.CreateDirectory(folderPath);
        }

        var firstFile = container.Files.FirstOrDefault();
        if (firstFile != null && firstFile.Length > 0)
        {
          var fileName = licenceRecord.GetVehicleRevenueLicenceImageName(Path.GetExtension(firstFile.FileName));
          var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await firstFile.CopyToAsync(stream);

            licenceRecord.Attachment = fileName;

            _db.VehicleRevenueLicence.Update(licenceRecord);

            await _db.SaveChangesAsync();

          }
        }

        response.IsSuccess = true;
        response.Message = "Insurance image has been uploaded succesfully.";
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while uploading the file. Please try again.";
      }

      return response;
    }

    public DownloadFileViewModel DownloadInsuranceImage(int id)
    {
      var response = new DownloadFileViewModel();
      try
      {
        var licenseRecord = _db.VehicleRevenueLicence.FirstOrDefault(t => t.Id == id);
        var imagePath = licenseRecord.GetVehicleRevenueLicenceImagePath(config);
        byte[] fileContents = null;
        MemoryStream ms = new MemoryStream();

        using (FileStream fs = File.OpenRead(imagePath))
        {
          fs.CopyTo(ms);
          fileContents = ms.ToArray();
          ms.Dispose();
          response.FileData = fileContents;
        }

        response.FileName = licenseRecord.Attachment;
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
      }

      return response;
    }
  }
}
