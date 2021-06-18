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
  public static class ProductSubCategoryExtension
  {
    public static ProductSubCategory ToModel(this ProductSubCategoryViewModel vm, ProductSubCategory model = null)
    {
      if (model == null)
        model = new ProductSubCategory();

      model.Id = vm.Id;
      model.Name = vm.Name;
      model.ProductCategoryId = vm.ProductCategoryId;
      model.Description = vm.Description;
      model.Picture = vm.Picture;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static ProductSubCategoryViewModel ToVm(this ProductSubCategory model, IConfiguration config, ProductSubCategoryViewModel vm = null)
    {
      if (vm == null)
        vm = new ProductSubCategoryViewModel();

      vm.Id = model.Id;
      vm.Name = model.Name;
      vm.Description = model.Description;
      if (!string.IsNullOrEmpty(model.Picture))
      {
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var imagePath = string.Format(@"{0}{1}\{2}\{3}\{4}\{5}",
            config.GetSection("FileUploadPath").Value,
            FolderNames.PRODUCT_CATEGORY,
            model.ProductCategoryId,
            FolderNames.PRODUCT_SUB_CATEGORY,
            model.Id,
            model.Picture);
        if (File.Exists(imagePath))
        {
          vm.Picture = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
        }
      }
      vm.Picture = model.Picture;
      vm.IsActive = model.IsActive.Value;

      return vm;
    }
  }
}
