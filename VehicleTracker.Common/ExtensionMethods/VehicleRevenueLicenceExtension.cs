using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
  public static class VehicleRevenueLicenceExtension
  {
    public static VehicleRevenueLicence ToModel(this VehicleRevenueLicenceViewModel vm, VehicleRevenueLicence model = null)
    {
      if (model == null)
        model = new VehicleRevenueLicence();

      model.Id = vm.Id;
      model.VehicleId = vm.VehicleId;
      model.ValidTill = new DateTime(vm.ValidTillYear, vm.ValidTillMonth, vm.ValidTillDay);
      model.RevenueLicenceDate = new DateTime(vm.RevenueLicenceYear, vm.RevenueLicenceMonth, vm.RevenueLicenceDay); ;
      model.CreatedOn = DateTime.UtcNow;
      model.UpdatedOn = DateTime.UtcNow;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static VehicleRevenueLicenceViewModel ToVm(this VehicleRevenueLicence model, IConfiguration config, VehicleRevenueLicenceViewModel vm = null)
    {
      if (vm == null)
        vm = new VehicleRevenueLicenceViewModel();

      vm.Id = model.Id;
      vm.VehicleId = model.VehicleId;
      vm.ValidTill = model.ValidTill.ToString("MMMM dd, yyyy"); 
      vm.RevenueLicenceDate = model.RevenueLicenceDate.ToString("MMMM dd, yyyy");
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.IsActive = model.IsActive;
      vm.ValidTillYear = model.ValidTill.Year;
      vm.ValidTillMonth = model.ValidTill.Month;
      vm.ValidTillDay = model.ValidTill.Day;
      vm.RegistrationNo = model.Vehicle.RegistrationNo;
      vm.RevenueLicenceYear = model.RevenueLicenceDate.Year;
      vm.RevenueLicenceMonth = model.RevenueLicenceDate.Month;
      vm.RevenueLicenceDay = model.RevenueLicenceDate.Day;
      vm.ImageName = model.Attachment;
      if (!string.IsNullOrEmpty(model.Attachment))
      {
        var imagePath = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.VEHICLE_REVENUE, model.Vehicle.Id, model.Attachment);

        vm.ImageURL = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
      }


      return vm;
    }


    public static string GetVehicleRevenueLicenceFolderPath(this VehicleRevenueLicence model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}", config.GetSection("FileUploadPath").Value, FolderNames.VEHICLE_REVENUE, model.Vehicle.Id);
    }

    public static string GetVehicleRevenueLicenceImagePath(this VehicleRevenueLicence model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.VEHICLE_REVENUE, model.Vehicle.Id, model.Attachment);
    }

    public static string GetVehicleRevenueLicenceImageName(this VehicleRevenueLicence model, string extension)
    {
      return string.Format(@"Revenue-Licence-Image-{0}{1}", model.RevenueLicenceDate.Year, extension);
    }
  }
}
