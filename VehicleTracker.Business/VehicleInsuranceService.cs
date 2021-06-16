using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business
{
  public class VehicleInsuranceService : IVehicleInsuranceService
  {
    readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly IConfiguration config;
    private readonly ILogger<VehicleInsuranceService> logger;

    public VehicleInsuranceService(VMDBContext db, IUserService userService, IConfiguration config, ILogger<VehicleInsuranceService> logger)
    {
      this._db = db;
      this._userService = userService;
      this.config = config;
      this.logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleInsurance(VehicleInsuranceViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);
        var model = _db.VehicleInsurances.FirstOrDefault(x => x.Id == vm.Id);
        if (model == null)
        {
          model = vm.ToModel();
          model.CreatedBy = user.Id;
          model.UpdatedBy = user.Id;
          _db.VehicleInsurances.Add(model);
          response.Message = "New Record has been added.";
        }
        else
        {
          model.InsuranceDate = new DateTime(vm.InsuranceYear, vm.InsuranceMonth, vm.InsuranceDay, 0, 0, 0);
          model.ValidTill = new DateTime(vm.ValidTillYear, vm.ValidTillMonth, vm.ValidTillDay, 0, 0, 0);
          model.UpdatedBy = user.Id;
          model.Note = vm.Note;
          model.UpdatedOn = DateTime.UtcNow;

          _db.VehicleInsurances.Update(model);
          response.Message = "Record has been updated.";
        }

        await _db.SaveChangesAsync();

        response.IsSuccess = true;

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

        var vt = _db.VehicleInsurances.FirstOrDefault(t => t.Id == id);
        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleInsurances.Update(vt);
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

    public List<VehicleInsuranceViewModel> GetAllVehicleInsurance(int vehicleId)
    {
      var query = _db.VehicleInsurances.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.InsuranceDate);
      var data = new List<VehicleInsuranceViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm(config));
      });


      return data;
    }

    public VehicleInsuranceViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleInsurances.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm(config);
      }
      else
      {
        return new VehicleInsuranceViewModel();
      }
    }

    public VehicleInsuranceViewModel GetVehicleInsuranceById(long id)
    {
      var vtvm = _db.VehicleInsurances.FirstOrDefault(t => t.Id == id).ToVm(config);

      return vtvm;
    }

    public async Task<ResponseViewModel> UploadInsuranceImage(FileContainerModel container, string userName)
    {
      var response = new ResponseViewModel();


      try
      {
        var user = _db.Users.FirstOrDefault(t => t.Username == userName);

        var insuranceRecord = _db.VehicleInsurances.FirstOrDefault(x => x.Id == container.Id);

        var folderPath = insuranceRecord.GetVehicleInsuranceFolderPath(config);

        if (!string.IsNullOrEmpty(insuranceRecord.Attachment))
        {
          var existingImagePath = string.Format(@"{0}\{1}", folderPath, insuranceRecord.Attachment);
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
          var fileName = insuranceRecord.GetVehicleInsuranceImageName(Path.GetExtension(firstFile.FileName));
          var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await firstFile.CopyToAsync(stream);

            insuranceRecord.Attachment = fileName;

            _db.VehicleInsurances.Update(insuranceRecord);

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
        var insuranceRecord = _db.VehicleInsurances.FirstOrDefault(t => t.Id == id);
        var imagePath = insuranceRecord.GetVehicleInsuranceImagePath(config);
        byte[] fileContents = null;
        MemoryStream ms = new MemoryStream();

        using (FileStream fs = File.OpenRead(imagePath))
        {
          fs.CopyTo(ms);
          fileContents = ms.ToArray();
          ms.Dispose();
          response.FileData = fileContents;
        }

        response.FileName = insuranceRecord.Attachment;
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
      }

      return response;
    }
  }
}

