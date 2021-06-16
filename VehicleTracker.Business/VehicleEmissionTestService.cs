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
  public class VehicleEmissionTestService : IVehicleEmissionTestService
  {
    readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly IConfiguration config;
    private readonly ILogger<IVehicleEmissionTestService> logger;

    public VehicleEmissionTestService(VMDBContext db, IUserService userService, IConfiguration config, ILogger<IVehicleEmissionTestService> logger)
    {
      this._db = db;
      this._userService = userService;
      this.config = config;
      this.logger = logger;
    }

    public async Task<VehicleResponseViewModel> SaveVehicleEmissionTest(VehicleEmissionTestViewModel vm, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var model = vm.ToModel();
        model.CreatedBy = user.Id;
        model.UpdatedBy = user.Id;

        _db.VehicleEmissiontTests.Add(model);
        await _db.SaveChangesAsync();

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

    public async Task<VehicleResponseViewModel> DeleteVehicleEmissionTest(long id, string userName)
    {
      var response = new VehicleResponseViewModel();
      try
      {
        var user = _userService.GetUserByUsername(userName);

        var vt = _db.VehicleEmissiontTests.FirstOrDefault(t => t.Id == id);

        vt.UpdatedBy = user.Id;
        vt.IsActive = false;
        vt.UpdatedOn = DateTime.UtcNow;
        _db.VehicleEmissiontTests.Update(vt);
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

    public List<VehicleEmissionTestViewModel> GetAllVehicleEmissionTest(int vehicleId)
    {
      var query = _db.VehicleEmissiontTests.Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

      var data = new List<VehicleEmissionTestViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm(config));
      });


      return data;
    }

    public VehicleEmissionTestViewModel GetLatestRecordForVehicle(long vehicleId)
    {
      var latestRecord = _db.VehicleEmissiontTests.Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
      if (latestRecord != null)
      {
        return latestRecord.ToVm(config);
      }
      else
      {
        return new VehicleEmissionTestViewModel();
      }
    }

    public VehicleEmissionTestViewModel GetVehicleEmissionTestById(long id)
    {
      var vtvm = _db.VehicleEmissiontTests.FirstOrDefault(t => t.Id == id).ToVm(config);

      return vtvm;
    }

    public async Task<ResponseViewModel> UploadEmissionTestImage(FileContainerModel container, string userName)
    {
      var response = new ResponseViewModel();


      try
      {
        var user = _db.Users.FirstOrDefault(t => t.Username == userName);

        var emissionTestReportRecord = _db.VehicleEmissiontTests.FirstOrDefault(x => x.Id == container.Id);

        var folderPath = emissionTestReportRecord.GetVehicleEmissionTestFolderPath(config);

        if (!string.IsNullOrEmpty(emissionTestReportRecord.Attachment))
        {
          var existingImagePath = string.Format(@"{0}\{1}", folderPath, emissionTestReportRecord.Attachment);

          if(File.Exists(existingImagePath))
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
          var fileName = emissionTestReportRecord.GetVehicleEmissionTestImageName(Path.GetExtension(firstFile.FileName));
          var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await firstFile.CopyToAsync(stream);

            emissionTestReportRecord.Attachment = fileName;

            _db.VehicleEmissiontTests.Update(emissionTestReportRecord);

            await _db.SaveChangesAsync();

          }
        }

        response.IsSuccess = true;
        response.Message = "Emission Test Report image has been uploaded succesfully.";
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while uploading the file. Please try again.";
      }

      return response;
    }

    public DownloadFileViewModel DownloadEmissionTestImage(int id)
    {
      var response = new DownloadFileViewModel();
      try
      {
        var emissionTestReportRecord = _db.VehicleEmissiontTests.FirstOrDefault(t => t.Id == id);
        var imagePath = emissionTestReportRecord.GetVehicleEmissionTestImagePath(config);
        byte[] fileContents = null;
        MemoryStream ms = new MemoryStream();

        using (FileStream fs = File.OpenRead(imagePath))
        {
          fs.CopyTo(ms);
          fileContents = ms.ToArray();
          ms.Dispose();
          response.FileData = fileContents;
        }

        response.FileName = emissionTestReportRecord.Attachment;
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
      }

      return response;
    }
  }
}
