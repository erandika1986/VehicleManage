using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model;
using System.Linq;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Users;
using Microsoft.Extensions.Logging;
using TimeZoneConverter;
using VehicleTracker.Model.Enums;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IO;

namespace VehicleTracker.Business
{
  public class UserService : IUserService
  {
    #region Member variable

    private readonly VMDBContext _db;
    private readonly ILogger<IUserService> _logger;
    private readonly IConfiguration _config;


    #endregion

    #region Constructor

    public UserService(VMDBContext db, ILogger<IUserService> logger, IConfiguration config)
    {
      this._db = db;
      this._logger = logger;
      this._config = config;
    }

    #endregion

    public void AddNewUser()
    {


      var user = new User()
      {
        Email = "erandika1986@gmail.com",
        FirstName = "Erandika",
        IsActive = true,
        LastName = "Sandaruwan",
        MobileNo = "0702605650",
        Password = CustomPasswordHasher.GenerateHash("system"),
        Username = "erandika1986@gmail.com",
      };

      user.UserRoles = new HashSet<UserRole>();

      var userRole = new UserRole()
      {
        IsActive = true,
        RoleId = 1,
        StartedDate = DateTime.UtcNow
      };

      user.UserRoles.Add(userRole);
      _db.SaveChanges();
    }

    public async Task<ResponseViewModel> SaveUser(UserViewModel vm)
    {
      var response = new ResponseViewModel();

      try
      {

        var user = _db.Users.FirstOrDefault(x => x.Id == vm.Id);

        if(user==null)
        {
          user = vm.ToNewModel();

          user.UserRoles = new HashSet<UserRole>();

          foreach (var role in vm.Roles)
          {
            var userRole = new UserRole()
            {
              IsActive = true,
              RoleId = role.Id,
              StartedDate = DateTime.UtcNow
            };

            user.UserRoles.Add(userRole);
            _db.Users.Add(user);
          }

          response.Message = "New user has been created.";
        }
        else
        {
          user.Email = vm.Email;
          user.FirstName = vm.FirstName;
          user.LastName = vm.LastName;
          user.MobileNo = vm.MobileNo;
          user.Email = vm.Email;
          user.Nicno = vm.Nicno;
          user.DrivingLicenceNo = vm.DrivingLicenceNo;

          var existingId = user.UserRoles.Select(x => x.RoleId).ToList();
          var newRoles = (from nr in vm.Roles where !existingId.Any(x => x == nr.Id) select nr);
          var deletedRoles = (from dr in existingId where !vm.Roles.Any(x => x.Id == dr) select dr);

          //Assigned New roles
          foreach(var role in newRoles)
          {
            var userRole = new UserRole()
            {
              IsActive = true,
              RoleId = role.Id,
              StartedDate = DateTime.UtcNow
            };

            user.UserRoles.Add(userRole);
          }

          foreach(var id in deletedRoles)
          {
            var userRole = user.UserRoles.FirstOrDefault(x => x.RoleId == id);
            user.UserRoles.Remove(userRole);
          }

          _db.Users.Update(user);
          response.Message = "User detail has been updated successfully.";
        }

        await _db.SaveChangesAsync();
        response.IsSuccess = true;

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured.Please try again.";
      }

      return response;
    }

    public async Task<ResponseViewModel> DeleteUser(long id)
    {
      var response = new ResponseViewModel();

      try
      {
        var user = this._db.Users.Find(id);

        user.IsActive = false;
        this._db.Users.Update(user);

        await _db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "User has been deleted";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Operation has been failed.Please try again.";
      }

      return response;
    }

    public User GetUserByUsername(string userName)
    {
      var user = _db.Users.FirstOrDefault(t => t.Username.ToLower() == userName.ToLower());

      return user;
    }

    public UserMasterDataViewModel GetUserMasterData()
    {
      var response = new UserMasterDataViewModel();

      if(!_db.TimeZoneDetails.Any())
      {
        var timeZones = TimeZoneInfo.GetSystemTimeZones();

        foreach(var item in timeZones)
        {
          var timeZoneDetail = new TimeZoneDetail()
          {
            DisplayName = item.DisplayName,
            TimeZoneId = item.Id
          };

          _db.TimeZoneDetails.Add(timeZoneDetail);
        }

        _db.SaveChanges();
      }

      foreach(var item in _db.TimeZoneDetails)
      {
        response.TimeZones.Add(new DropDownViewModal() { Id = item.Id, Name = item.DisplayName });
      }

      foreach (RoleType role in (RoleType[])Enum.GetValues(typeof(RoleType)))
      {
        response.Roles.Add(new DropDownViewModal() { Id = (int)role, Name = EnumHelper.GetEnumDescription(role) });
      }

      return response;
    }

    public List<UserViewModel> GetAllUsers(int roleId, int status)
    {
      var response = new List<UserViewModel>();

      var query = _db.Users.OrderBy(x=>x.FirstName);

      if(status>0)
      {
        var isActive = status == 1 ? true : false;
        query = query.Where(x => x.IsActive == isActive).OrderBy(x => x.FirstName);
      }

      if(roleId>0)
      {
        query = query.Where(x => x.UserRoles.Any(x => x.RoleId == roleId)).OrderBy(x=>x.FirstName);
      }

      var users = query.ToList();

      foreach (var item in users)
      {
        response.Add(item.ToVm(_config));
      }

      return response;
    }

    public async Task<ResponseViewModel> UploadUserImage(FileContainerModel container, string userName)
    {
      var response = new ResponseViewModel();


      try
      {
        var user = _db.Users.FirstOrDefault(t => t.Username == userName);
        var userRecord = _db.Users.FirstOrDefault(x => x.Id == container.Id);
        //var folderPath = string.Empty;
        //var fileName = string.Empty;
        //var filePath = string.Empty;

        var firstFile = container.Files.FirstOrDefault();

        switch (container.Type)
        {
          case (int)UserPhotoType.UserImage:
            {
              var folderPath = userRecord.GetUserImagePath(_config);
              if (!string.IsNullOrEmpty(userRecord.Image))
              {
                var existingImagePath = string.Format(@"{0}\{1}", folderPath, userRecord.Image);
                if (File.Exists(existingImagePath))
                {
                  File.Delete(existingImagePath);
                }
              }

              if (!Directory.Exists(folderPath))
              {
                Directory.CreateDirectory(folderPath);
              }

              if (firstFile != null && firstFile.Length > 0)
              {
                var fileName = userRecord.GetUserImageName(Path.GetExtension(firstFile.FileName));
                var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                  await firstFile.CopyToAsync(stream);

                  userRecord.Image = fileName;

                  _db.Users.Update(userRecord);

                  await _db.SaveChangesAsync();

                  response.Message = "User image has been uploaded succesfully.";
                }
              }
            }
            break;
          case (int)UserPhotoType.NicBack:
            {
              var folderPath = userRecord.GetUserNICBackImagePath(_config);
              if (!string.IsNullOrEmpty(userRecord.NicbackImage))
              {
                var existingImagePath = string.Format(@"{0}\{1}", folderPath, userRecord.NicbackImage);
                if (File.Exists(existingImagePath))
                {
                  File.Delete(existingImagePath);
                }
              }

              if (!Directory.Exists(folderPath))
              {
                Directory.CreateDirectory(folderPath);
              }

              if (firstFile != null && firstFile.Length > 0)
              {
                var fileName = userRecord.GetNICBackImageName(Path.GetExtension(firstFile.FileName));
                var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                  await firstFile.CopyToAsync(stream);

                  userRecord.Image = fileName;

                  _db.Users.Update(userRecord);

                  await _db.SaveChangesAsync();

                  response.Message = "User NIC back image has been uploaded succesfully.";
                }
              }
            }
            break;
          case (int)UserPhotoType.NicFront:
            {
              var folderPath = userRecord.GetUserNICFrontImagePath(_config);
              if (!string.IsNullOrEmpty(userRecord.NicfrontImage))
              {
                var existingImagePath = string.Format(@"{0}\{1}", folderPath, userRecord.NicfrontImage);
                if (File.Exists(existingImagePath))
                {
                  File.Delete(existingImagePath);
                }
              }

              if (!Directory.Exists(folderPath))
              {
                Directory.CreateDirectory(folderPath);
              }

              if (firstFile != null && firstFile.Length > 0)
              {
                var fileName = userRecord.GetNICFrontImageName(Path.GetExtension(firstFile.FileName));
                var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                  await firstFile.CopyToAsync(stream);

                  userRecord.Image = fileName;

                  _db.Users.Update(userRecord);

                  await _db.SaveChangesAsync();

                  response.Message = "User NIC front image has been uploaded succesfully.";
                }
              }
            }
            break;
          case (int)UserPhotoType.DrivingLicenceFront:
            {
              var folderPath = userRecord.GetUserDrivingLicenceFrontImagePath(_config);
              if (!string.IsNullOrEmpty(userRecord.DrivingLicenceFrontImage))
              {
                var existingImagePath = string.Format(@"{0}\{1}", folderPath, userRecord.DrivingLicenceFrontImage);
                if (File.Exists(existingImagePath))
                {
                  File.Delete(existingImagePath);
                }
              }

              if (!Directory.Exists(folderPath))
              {
                Directory.CreateDirectory(folderPath);
              }

              if (firstFile != null && firstFile.Length > 0)
              {
                var fileName = userRecord.GetDrivingLicenceFrontImageName(Path.GetExtension(firstFile.FileName));
                var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                  await firstFile.CopyToAsync(stream);

                  userRecord.Image = fileName;

                  _db.Users.Update(userRecord);

                  await _db.SaveChangesAsync();

                  response.Message = "User driving licence front image has been uploaded succesfully.";
                }
              }
            }
            break;
          case (int)UserPhotoType.DrivingLicenceBack:
            {
              var folderPath = userRecord.GetUserDrivingLicenceBackImagePath(_config);
              if (!string.IsNullOrEmpty(userRecord.DrivingLicenceFrontImage))
              {
                var existingImagePath = string.Format(@"{0}\{1}", folderPath, userRecord.DrivingLicenceBackImage);
                if (File.Exists(existingImagePath))
                {
                  File.Delete(existingImagePath);
                }
              }

              if (!Directory.Exists(folderPath))
              {
                Directory.CreateDirectory(folderPath);
              }

              if (firstFile != null && firstFile.Length > 0)
              {
                var fileName = userRecord.GetDrivingLicenceBackImageName(Path.GetExtension(firstFile.FileName));
                var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                  await firstFile.CopyToAsync(stream);

                  userRecord.Image = fileName;

                  _db.Users.Update(userRecord);

                  await _db.SaveChangesAsync();

                  response.Message = "User driving licence back image has been uploaded succesfully.";
                }
              }
            }
            break;
        }

        response.IsSuccess = true;

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while uploading the file. Please try again.";
      }

      return response;
    }

    public DownloadFileViewModel DownloadUserImage(int id, int type)
    {
      var response = new DownloadFileViewModel();
      try
      {
        var imagePath = string.Empty;
        var userRecord = _db.Users.FirstOrDefault(t => t.Id == id);
      

        switch (type)
        {
          case (int)UserPhotoType.UserImage:
            {
              imagePath = userRecord.GetUserImagePath(_config);
              response.FileName = userRecord.Image;
            }
            break;
          case (int)UserPhotoType.NicBack:
            {
              imagePath = userRecord.GetUserNICBackImagePath(_config);
              response.FileName = userRecord.NicfrontImage;
            }
            break;
          case (int)UserPhotoType.NicFront:
            {
              imagePath = userRecord.GetUserNICFrontImagePath(_config);
              response.FileName = userRecord.NicbackImage;
            }
            break;
          case (int)UserPhotoType.DrivingLicenceFront:
            {
              imagePath = userRecord.GetUserDrivingLicenceFrontImagePath(_config);
              response.FileName = userRecord.DrivingLicenceFrontImage;
            }
            break;
          case (int)UserPhotoType.DrivingLicenceBack:
            {
              imagePath = userRecord.GetUserDrivingLicenceBackImagePath(_config);
              response.FileName = userRecord.DrivingLicenceBackImage;
            }
            break;
        }

        byte[] fileContents = null;
        MemoryStream ms = new MemoryStream();

        using (FileStream fs = File.OpenRead(imagePath))
        {
          fs.CopyTo(ms);
          fileContents = ms.ToArray();
          ms.Dispose();
          response.FileData = fileContents;
        }


      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
      }

      return response;
    }
  }

  public class UserMasterDataViewModel
  {
    public UserMasterDataViewModel()
    {
      Roles = new List<DropDownViewModal>();
      TimeZones = new List<DropDownViewModal>();
    }

    public List<DropDownViewModal> Roles { get; set; }
    public List<DropDownViewModal> TimeZones { get; set; }
  }

  public enum UserPhotoType
  {
    UserImage=1,
    NicFront=2,
    NicBack=3,
    DrivingLicenceFront=4,
    DrivingLicenceBack=5
  }
}
