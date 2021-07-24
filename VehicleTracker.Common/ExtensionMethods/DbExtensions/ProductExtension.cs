using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel;
using System.Linq;

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
      model.ProductCode = vm.ProductCode;
      model.UnitPrice = vm.UnitPrice;
      model.AvailableQty = vm.AvailableQty;
      model.Description = vm.Description;
      model.SubProductCategoryId = vm.ProductSubCategoryId;
      model.SupplierId = vm.SupplierId;
      model.IsActive = vm.IsActive;
      model.MinOrderQty = vm.MinOrderQty;
      model.MaxOrderQty = vm.MaxOrderQty;

      return model;
    }

    public static ProductViewModel ToVm(this Product model, IConfiguration config, ProductViewModel vm = null)
    {
      if (vm == null)
        vm = new ProductViewModel();

      vm.Id = model.Id;
      vm.Name = model.ProductName;
      vm.Description = model.Description;
      vm.UnitPrice = model.UnitPrice;
      vm.AvailableQty = model.AvailableQty;
      vm.ProductSubCategoryId = model.SubProductCategoryId;
      vm.SupplierId = model.SupplierId;
      vm.ProductCode = model.ProductCode;
      vm.SubCategoryName = model.SubProductCategory.Name;
      vm.CategoryName = model.SubProductCategory.ProductCategory.Name;
      vm.SupplierName = model.Supplier.Name;
      vm.IsActive = model.IsActive.Value;
      vm.MaxOrderQty = model.MaxOrderQty;
      vm.MinOrderQty = model.MinOrderQty;

      if(model.ProductImages.Count>0)
      {
        var image = model.ProductImages.FirstOrDefault(x => x.IsDefault == true);
        if(image==null)
        {
          image = model.ProductImages.FirstOrDefault();
        }
        //vm.ImageURL = string.Format("{0}/{1}/{2}/{3}", config.GetSection("FileUploadUrl").Value, FolderNames.INSURANCE, model.Vehicle.Id, model.Attachment);
        var imagePath = string.Format(@"{0}{1}\{2}\{3}\{4}\{5}\{6}",
                config.GetSection("FileUploadPath").Value,
                FolderNames.PRODUCT_CATEGORY,
                model.SubProductCategory.ProductCategoryId,
                FolderNames.PRODUCT_SUB_CATEGORY,
                model.SubProductCategoryId,
                model.Id,
                image.Name);

        if (File.Exists(imagePath))
        {
          vm.DefaultImage = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
        }
      }

      //foreach (var item in model.ProductImages)
      //{
      //  var image = new ProductImageViewModel()
      //  {
      //    Id = item.Id,
      //    ImageName = item.Name,
      //    ProductId = model.Id
      //  };

      //  var imagePath = string.Format(@"{0}{1}\{2}\{3}\{4}\{5}\{6}",
      //          config.GetSection("FileUploadPath").Value,
      //          FolderNames.PRODUCT_CATEGORY,
      //          model.SubProductCategory.ProductCategoryId,
      //          FolderNames.PRODUCT_SUB_CATEGORY,
      //          model.SubProductCategoryId,
      //          model.Id,
      //          item.Name);
      //  if (File.Exists(imagePath))
      //  {
      //    vm.DefaultImage = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
      //  }
      //}

      return vm;
    }

    public static List<ProductImageViewModel> GetProductImages(this Product model, IConfiguration config)
    {
      var response = new List<ProductImageViewModel>();

      foreach (var item in model.ProductImages)
      {
        var image = new ProductImageViewModel()
        {
          Id = item.Id,
          ImageName = item.Name,
          ProductId = model.Id,
          IsDefault=item.IsDefault
        };

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
          image.Image = "data:image/jpg;base64," + ImageHelper.getThumnialImage(imagePath);
        }

        response.Add(image);
      }

      return response;
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
