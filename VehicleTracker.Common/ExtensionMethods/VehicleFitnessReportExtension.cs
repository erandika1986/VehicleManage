using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
  public static class VehicleFitnessReportExtension
  {
    public static VehicleFitnessReport ToModel(this VehicleFitnessReportViewModel vm, VehicleFitnessReport model = null)
    {
      if (model == null)
        model = new VehicleFitnessReport();

      model.Id = vm.Id;
      model.VehicleId = vm.VehicleId;
      model.ValidTill = new DateTime(vm.ValidTillYear, vm.ValidTillMonth, vm.ValidTillDay);
      model.FitnessReportDate = new DateTime(vm.FitnessReportYear, vm.FitnessReportMonth, vm.FitnessReportDay);
      model.CreatedOn = DateTime.UtcNow;
      model.UpdatedOn = DateTime.UtcNow;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static VehicleFitnessReportViewModel ToVm(this VehicleFitnessReport model, IConfiguration config, VehicleFitnessReportViewModel vm = null)
    {
      if (vm == null)
        vm = new VehicleFitnessReportViewModel();

      vm.Id = model.Id;
      vm.VehicleId = model.VehicleId;
      vm.ValidTill = model.ValidTill.ToString("MMMM dd, yyyy");
      vm.FitnessReportDate = model.FitnessReportDate.ToString("MMMM dd, yyyy");
      vm.CreatedOn = model.CreatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedOn = model.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.IsActive = model.IsActive;
      vm.ValidTillYear = model.ValidTill.Year;
      vm.ValidTillMonth = model.ValidTill.Month;
      vm.ValidTillDay = model.ValidTill.Day;
      vm.RegistrationNo = model.Vehicle.RegistrationNo;
      vm.FitnessReportYear = model.FitnessReportDate.Year;
      vm.FitnessReportMonth = model.FitnessReportDate.Month;
      vm.FitnessReportDay = model.FitnessReportDate.Day;
      vm.ImageName = model.Attachment;
      if (!string.IsNullOrEmpty(model.Attachment))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var imagePath = string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.FITNESS_REPORT, model.Vehicle.Id, model.Attachment);

        vm.ImageURL = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
      }

      return vm;
    }

    public static string GetVehicleFitnessReportFolderPath(this VehicleFitnessReport model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}", config.GetSection("FileUploadPath").Value, FolderNames.FITNESS_REPORT, model.Vehicle.Id);
    }

    public static string GetVehicleFitnessReportImagePath(this VehicleFitnessReport model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.FITNESS_REPORT, model.Vehicle.Id, model.Attachment);
    }

    public static string GetVehicleFitnessReportImageName(this VehicleFitnessReport model, string extension)
    {
      return string.Format(@"FitnessReport-Image-{0}{1}", model.FitnessReportDate.Year, extension);
    }
  }
}
