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
  public static class ProductExtension
  {
    public static Product ToModel(this ProductViewModel vm, Product model = null)
    {
      if (model == null)
        model = new Product();

      model.Id = vm.Id;
      model.ProductName = vm.Name;
      model.ProductCode = vm.Description;
      model.UnitPrice = vm.UnitPrice;
      model.AvailableQty = vm.AvailableQty;
      model.SubProductCategoryId = vm.ProductSubCategoryId;
      model.SupplierId = vm.SupplierId;
      model.IsActive = vm.IsActive;

      return model;
    }

    public static ProductViewModel ToVm(this Product model, IConfiguration config, ProductViewModel vm = null)
    {
      if (vm == null)
        vm = new ProductViewModel();

      vm.Id = model.Id;
      vm.Name = model.ProductName;
      vm.Description = model.ProductCode;
      vm.UnitPrice = model.UnitPrice;
      vm.AvailableQty = model.AvailableQty;
      vm.ProductSubCategoryId = model.SubProductCategoryId;
      vm.SupplierId = model.SupplierId;
      vm.IsActive = model.IsActive.Value;

      foreach (var item in model.ProductImages)
      {
        var image = new ProductImageViewModel()
        {
          Id = item.Id,
          ImageName = item.Name,
          ProductId = model.Id
        };

        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var imagePath = string.Format(@"{0}{1}\{2}\{3}\{4}\{5}\{6}",
                config.GetSection("FileUploadPath").Value,
                FolderNames.PRODUCT_CATEGORY,
                model.SubProductCategory.ProductCategoryId,
                FolderNames.PRODUCT_SUB_CATEGORY,
                model.SubProductCategoryId,
                model.Id,
                item.Name);
        if (File.Exists(imagePath))
        {
          vm.Picture = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
        }
      }

      return vm;
    }

    public static string GetProductFolderPath(this Product model, IConfiguration config)
    {
      var folderPath = string.Format(@"{0}{1}\{2}\{3}\{4}\{5}",
              config.GetSection("FileUploadPath").Value,
              FolderNames.PRODUCT_CATEGORY,
              model.SubProductCategory.ProductCategoryId,
              FolderNames.PRODUCT_SUB_CATEGORY,
              model.SubProductCategoryId,
              model.Id);

      return folderPath;
    }

    public static string GetProductImagePath(this ProductImage model, IConfiguration config)
    {
      var folderPath = string.Format(@"{0}{1}\{2}\{3}\{4}\{5}\{6}",
              config.GetSection("FileUploadPath").Value,
              FolderNames.PRODUCT_CATEGORY,
              model.Product.SubProductCategory.ProductCategoryId,
              FolderNames.PRODUCT_SUB_CATEGORY,
              model.Product.SubProductCategoryId,
              model.Product.Id,
              model.Name);

      return folderPath;
    }
  }
}
