using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Users;

namespace System
{
  public static class UserExtension
  {
    public static User ToNewModel(this UserViewModel vm, User model = null)
    {
      if (model == null)
        model = new User();



      model.Email = vm.Email;
      model.FirstName = vm.FirstName;
      model.IsActive = true;
      model.LastName = vm.LastName;
      model.MobileNo = vm.MobileNo;
      model.DrivingLicenceNo = vm.DrivingLicenceNo;
      model.Nicno = vm.Nicno;
      model.PersonalAddress = vm.PersonalAddress;
      model.TimeZoneId = vm.TimeZoneId;
      model.Username = vm.Username;


      return model;
    }

    public static UserViewModel ToVm(this User model, IConfiguration config, UserViewModel vm = null)
    {
      if (vm == null)
        vm = new UserViewModel();

      vm.Email = model.Email;
      vm.FirstName = model.FirstName;
      vm.IsActive = model.IsActive;
      vm.LastName = model.LastName;
      vm.MobileNo = model.MobileNo;
      vm.DrivingLicenceNo = model.DrivingLicenceNo;
      vm.Nicno = model.Nicno;
      vm.PersonalAddress = model.PersonalAddress;
      vm.TimeZoneId = model.TimeZoneId.HasValue?model.TimeZoneId.Value:0;
      vm.Username = model.Username;


      if (!string.IsNullOrEmpty(model.Image))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var userImage = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.Image);
        if (File.Exists(userImage))
        {
          vm.Image = "data:image/jpg;base64," + ImageHelper.getThumnialImage(userImage);
        }
      }

      if (!string.IsNullOrEmpty(model.NicfrontImage))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var niceFrontImage = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.NicfrontImage);
        if (File.Exists(niceFrontImage))
        {
          vm.NicFrontImage = "data:image/jpg;base64," + ImageHelper.getThumnialImage(niceFrontImage);
        }
      }

      if (!string.IsNullOrEmpty(model.NicbackImage))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var nicBackImage = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.NicbackImage);
        if (File.Exists(nicBackImage))
        {
          vm.NicBackImage = "data:image/jpg;base64," + ImageHelper.getThumnialImage(nicBackImage);
        }
      }


      if (!string.IsNullOrEmpty(model.DrivingLicenceFrontImage))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var drivingFrontImage = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.DrivingLicenceFrontImage);
        if (File.Exists(drivingFrontImage))
        {
          vm.DrivingLicenceFrontImage = "data:image/jpg;base64," + ImageHelper.getThumnialImage(drivingFrontImage);
        }
      }

      if (!string.IsNullOrEmpty(model.DrivingLicenceBackImage))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var drivingBackImage = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.DrivingLicenceBackImage);
        if (File.Exists(drivingBackImage))
        {
          vm.DrivingLicenceBackImage = "data:image/jpg;base64," + ImageHelper.getThumnialImage(drivingBackImage);
        }
      }

      return vm;
    }

    public static string GetUserImageFolderPath(this User model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id);
    }

    public static string GetUserImagePath(this User model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.Image);
    }

    public static string GetUserNICFrontImagePath(this User model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.NicfrontImage);
    }

    public static string GetUserNICBackImagePath(this User model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.NicbackImage);
    }

    public static string GetUserDrivingLicenceFrontImagePath(this User model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.DrivingLicenceFrontImage);
    }

    public static string GetUserDrivingLicenceBackImagePath(this User model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.USERS, model.Id, model.DrivingLicenceBackImage);
    }



    public static string GetUserImageName(this User model, string extension)
    {
      return string.Format(@"User-Image-{0}{1}", model.Id, extension);
    }

    public static string GetNICFrontImageName(this User model, string extension)
    {
      return string.Format(@"Nic-Front-Image-{0}{1}", model.Id, extension);
    }

    public static string GetNICBackImageName(this User model, string extension)
    {
      return string.Format(@"Nic-Back-Image-{0}{1}", model.Id, extension);
    }

    public static string GetDrivingLicenceFrontImageName(this User model, string extension)
    {
      return string.Format(@"Driving-Licence-Front-Image-{0}{1}", model.Id, extension);
    }

    public static string GetDrivingLicenceBackImageName(this User model, string extension)
    {
      return string.Format(@"Driving-Licence-Back-Image-{0}{1}", model.Id, extension);
    }
  }
}
