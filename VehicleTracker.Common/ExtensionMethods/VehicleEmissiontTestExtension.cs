using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
  public static class VehicleEmissiontTestExtension
  {
    public static VehicleEmissiontTest ToModel(this VehicleEmissionTestViewModel vm, VehicleEmissiontTest model = null)
    {
      if (model == null)
        model = new VehicleEmissiontTest();

      model.Id = vm.Id;
      model.VehicleId = vm.VehicleId;
      model.ValidTill = new DateTime(vm.ValidTillYear, vm.ValidTillMonth, vm.ValidTillDay); ;
      model.EmissiontTestDate = new DateTime(vm.EmissionTestYear, vm.EmissionTestMonth, vm.EmissionTestDay);
      model.CreatedOn = DateTime.UtcNow;
      model.UpdatedOn = DateTime.UtcNow;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static VehicleEmissionTestViewModel ToVm(this VehicleEmissiontTest model, IConfiguration config, VehicleEmissionTestViewModel vm = null)
    {
      if (vm == null)
        vm = new VehicleEmissionTestViewModel();

      vm.Id = model.Id;
      vm.VehicleId = model.VehicleId;
      vm.ValidTill = model.ValidTill.ToString("MMMM dd, yyyy");
      vm.EmissiontTestDate = model.EmissiontTestDate.ToString("MMMM dd, yyyy");
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.CreatedBy = string.Format("{0} {1}", model.CreatedByNavigation.FirstName, model.CreatedByNavigation.LastName);
      vm.UpdatedBy = string.Format("{0} {1}", model.UpdatedByNavigation.FirstName, model.UpdatedByNavigation.LastName);
      vm.IsActive = model.IsActive;
      vm.ValidTillYear = model.ValidTill.Year;
      vm.ValidTillMonth = model.ValidTill.Month;
      vm.ValidTillDay = model.ValidTill.Day;
      vm.RegistrationNo = model.Vehicle.RegistrationNo;
      vm.EmissionTestYear = model.EmissiontTestDate.Year;
      vm.EmissionTestMonth = model.EmissiontTestDate.Month;
      vm.EmissionTestDay = model.EmissiontTestDate.Day;
      vm.ImageName = model.Attachment;

      if (!string.IsNullOrEmpty(model.Attachment))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var imagePath = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.EMISSION_TEST, model.Vehicle.Id, model.Attachment);
        if (File.Exists(imagePath))
        {
          vm.ImageURL = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
        }

      }


      return vm;
    }

    public static string GetVehicleEmissionTestFolderPath(this VehicleEmissiontTest model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}", config.GetSection("FileUploadPath").Value, FolderNames.EMISSION_TEST, model.Vehicle.Id);
    }

    public static string GetVehicleEmissionTestImagePath(this VehicleEmissiontTest model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.EMISSION_TEST, model.Vehicle.Id, model.Attachment);
    }

    public static string GetVehicleEmissionTestImageName(this VehicleEmissiontTest model, string extension)
    {
      return string.Format(@"EmissiontTest-Image-{0}{1}", model.EmissiontTestDate.Year, extension);
    }
  }
}
