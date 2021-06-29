using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business
{
  public class ProductSubCategoryService : IProductSubCategoryService
  {
    #region Member variable

    private readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    private readonly ILogger<IProductSubCategoryService> _logger;

    #endregion


    public ProductSubCategoryService(VMDBContext db, IUserService userService, IConfiguration config, ILogger<IProductSubCategoryService> logger)
    {
      this._db = db;
      this._userService = userService;
      this._config = config;
      this._logger = logger;
    }

    public async Task<ResponseViewModel> DeleteProductSubCategory(int id, string userName)
    {
      var response = new ResponseViewModel();

      try
      {

        var category = _db.ProductSubCategories.FirstOrDefault(d => d.Id == id);
        category.IsActive = false;

        _db.ProductSubCategories.Update(category);
        await _db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Product sub category has been deleted.";
      }
      catch (Exception ex)
      {

        response.IsSuccess = true;
        response.Message = "Error has been occured while deleting the data. Please try again.";
      }

      return response;
    }

    public List<ProductSubCategoryViewModel> GetAllProductSubCategories(int categoryId)
    {
      var query = _db.ProductSubCategories.Where(t => t.IsActive == true).OrderBy(t => t.Name);

      if (categoryId > 0)
      {
        query = query.Where(x => x.ProductCategoryId == categoryId).OrderBy(t => t.Name);
      }

      var data = new List<ProductSubCategoryViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm(_config));
      });

      return data;
    }

    public ProductSubCategoryViewModel GetProductSubCategoryById(int id)
    {
      var response = new ProductSubCategoryViewModel();

      var pCategory = _db.ProductSubCategories.FirstOrDefault(x => x.Id == id);

      response.Description = pCategory.Description;
      response.Id = pCategory.Id;
      response.IsActive = pCategory.IsActive.Value;
      response.Name = pCategory.Name;
      response.Picture = pCategory.Picture;
      response.ProductCategoryId = pCategory.ProductCategoryId;

      return response;
    }

    public async Task<ResponseViewModel> SaveProductSubCategory(ProductSubCategoryViewModel vm, string userName)
    {
      var response = new ResponseViewModel();

      try
      {

        var category = _db.ProductSubCategories.FirstOrDefault(d => d.Id == vm.Id);

        if (category == null)
        {
          category = new Model.ProductSubCategory()
          {
            Name = vm.Name,
            Description = vm.Description,
            IsActive = true,
            Picture = vm.Picture,
            ProductCategoryId = vm.ProductCategoryId
          };

          _db.ProductSubCategories.Add(category);
          await _db.SaveChangesAsync();

          response.IsSuccess = true;
          response.Message = "New Product sub category has been added.";
        }
        else
        {
          category.Description = vm.Description;
          category.Name = vm.Name;
          category.Picture = vm.Picture;
          category.ProductCategoryId = vm.ProductCategoryId;

          _db.ProductSubCategories.Update(category);
          await _db.SaveChangesAsync();

          response.IsSuccess = true;
          response.Message = "Product sub category has been updated.";
        }
      }
      catch (Exception ex)
      {

        response.IsSuccess = true;
        response.Message = "Error has been occured while saving the data. Please try again.";
      }

      return response;
    }

    public List<DropDownViewModal> GetProductCategories()
    {
      var response = new List<DropDownViewModal>();
      response.Add(new DropDownViewModal() { Id = 0, Name = "--All--" });
      var categories = _db.ProductCategories
                      .Where(c=>c.IsActive==true)
                      .OrderBy(o=>o.Name)
                      .Select(x => new DropDownViewModal() { Id = x.Id, Name = x.Name }).ToList();
      response.AddRange(categories);
      return response;
    }

    public async Task<ResponseViewModel> UploadSubProductCategoryImage(FileContainerModel container, string userName)
    {
      var response = new ResponseViewModel();


      try
      {
        var user = _db.Users.FirstOrDefault(t => t.Username == userName);

        var productSubCategoryRecord = _db.ProductSubCategories.FirstOrDefault(x => x.Id == container.Id);

        var folderPath = productSubCategoryRecord.GetProductSubCategoryImageFolderPath(_config);

        if (!string.IsNullOrEmpty(productSubCategoryRecord.Picture))
        {
          var existingImagePath = string.Format(@"{0}\{1}", folderPath, productSubCategoryRecord.Picture);

          if (File.Exists(existingImagePath))
          {
            File.Delete(existingImagePath);
          }
        }

        if (!Directory.Exists(folderPath))
        {
          Directory.CreateDirectory(folderPath);
        }

        var firstFile = container.Files.FirstOrDefault();
        if (firstFile != null && firstFile.Length > 0)
        {
          var fileName = productSubCategoryRecord.GetProductSubCategoryImageName(Path.GetExtension(firstFile.FileName));
          var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await firstFile.CopyToAsync(stream);

            productSubCategoryRecord.Picture = fileName;

            _db.ProductSubCategories.Update(productSubCategoryRecord);

            await _db.SaveChangesAsync();

          }
        }

        response.IsSuccess = true;
        response.Message = "Product category image has been uploaded succesfully.";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while uploading the file. Please try again.";
      }

      return response;
    }

    public DownloadFileViewModel DownloadProductSubCategoryImage(int id)
    {
      throw new NotImplementedException();
    }
  }
}
