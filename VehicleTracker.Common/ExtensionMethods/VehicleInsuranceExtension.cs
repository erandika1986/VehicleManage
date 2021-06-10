using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
  public static class VehicleInsuranceExtension
  {
    public static VehicleInsurance ToModel(this VehicleInsuranceViewModel vm, VehicleInsurance model = null)
    {
      if (model == null)
        model = new VehicleInsurance();

      model.Id = vm.Id;
      model.VehicleId = vm.VehicleId;
      model.ValidTill = new DateTime(vm.ValidTillYear, vm.ValidTillMonth, vm.ValidTillDay);
      model.InsuranceDate = new DateTime(vm.InsuranceYear, vm.InsuranceMonth, vm.InsuranceDay);

      model.CreatedOn = DateTime.UtcNow;
      model.UpdatedOn = DateTime.UtcNow;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static VehicleInsuranceViewModel ToVm(this VehicleInsurance model, IConfiguration config, VehicleInsuranceViewModel vm = null)
    {
      if (vm == null)
        vm = new VehicleInsuranceViewModel();

      vm.Id = model.Id;
      vm.VehicleId = model.VehicleId;

      vm.ValidTill = model.ValidTill.ToString("MMMM dd, yyyy");
      vm.InsuranceDate = model.InsuranceDate.ToString("MMMM dd, yyyy");
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.IsActive = model.IsActive;
      vm.ValidTillYear = model.ValidTill.Year;
      vm.ValidTillMonth = model.ValidTill.Month;
      vm.ValidTillDay = model.ValidTill.Day;
      vm.RegistrationNo = model.Vehicle.RegistrationNo;
      vm.InsuranceYear = model.InsuranceDate.Year;
      vm.InsuranceMonth = model.InsuranceDate.Month;
      vm.InsuranceDay = model.InsuranceDate.Day;
      vm.ImageName = model.Attachment;
      if (!string.IsNullOrEmpty(model.Attachment))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var imagePath = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);

        vm.ImageURL = "data:image/jpg;base64,"+ ImageHelper.getThumnialImage(imagePath);
      }

      return vm;

    }

    public static string GetVehicleInsuranceFolderPath(this VehicleInsurance model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}", config.GetSection("FileUploadPath").Value, FolderNames.INSURANCE, model.Vehicle.Id);
    }

    public static string GetVehicleInsuranceImagePath(this VehicleInsurance model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
    }

    public static string GetVehicleInsuranceImageName(this VehicleInsurance model,string extension)
    {
      return string.Format(@"Insurance-Image-{0}{1}",model.InsuranceDate.Year,extension);
    }
  }


}
