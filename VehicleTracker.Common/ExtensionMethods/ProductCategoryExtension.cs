using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel;

namespace System
{
  public static class ProductCategoryExtension
  {
    public static ProductCategory ToModel(this ProductCategoryViewModel vm, ProductCategory model = null)
    {
      if (model == null)
        model = new ProductCategory();

      model.Id = vm.Id;
      model.Name = vm.Name;
      model.Description = vm.Description;
      model.Picture = vm.Picture;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static ProductCategoryViewModel ToVm(this ProductCategory model, IConfiguration config, ProductCategoryViewModel vm = null)
    {
      if (vm == null)
        vm = new ProductCategoryViewModel();

      vm.Id = model.Id;
      vm.Name = model.Name;
      vm.Description = model.Description;
      if (!string.IsNullOrEmpty(model.Picture))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var imagePath = string.Format(@"{0}{1}\{2}\{3}",
            config.GetSection("FileUploadPath").Value,
            FolderNames.PRODUCT_CATEGORY,
            model.Id,
            model.Picture);

        if (File.Exists(imagePath))
        {
          vm.Picture = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
        }
      }
      vm.IsActive = model.IsActive.Value;

      return vm;
    }

    public static string GetProductCategoryImageFolderPath(this ProductCategory model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}", config.GetSection("FileUploadPath").Value, FolderNames.PRODUCT_CATEGORY, model.Id);
    }

    public static string GetProductCategoryImagePath(this ProductCategory model, IConfiguration config)
    {
      return string.Format(@"{0}{1}\{2}\{3}", config.GetSection("FileUploadPath").Value, FolderNames.PRODUCT_CATEGORY, model.Id, model.Picture);
    }

    public static string GetProductCategoryImageName(this ProductCategory model, string extension)
    {
      return string.Format(@"ProductCategory-{0}{1}", model.Name, extension);
    }
  }
}
