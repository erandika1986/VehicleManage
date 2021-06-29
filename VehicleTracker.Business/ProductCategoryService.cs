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
  public class ProductCategoryService : IProductCategoryService
  {

    #region Member variable

    private readonly VMDBContext _db;
    private readonly IConfiguration _config;
    private readonly IUserService _userService;
    private readonly IProductSubCategoryService _productSubCategoryService;
    private readonly ILogger<IProductCategoryService> _logger;

    #endregion

    public ProductCategoryService(VMDBContext db, IUserService userService, IProductSubCategoryService productSubCategoryService, ILogger<IProductCategoryService> logger, IConfiguration config)
    {

      this._db = db;
      this._logger = logger;
      this._config = config;
      this._productSubCategoryService = productSubCategoryService;
      this._userService = userService;
    }

    public async Task<ResponseViewModel> DeleteProductCategory(int id, string userName)
    {
      var response = new ResponseViewModel();

      try
      {

        var category = _db.ProductCategories.FirstOrDefault(d => d.Id == id);

        category = new Model.ProductCategory()
        {

          IsActive = false
        };

        _db.ProductCategories.Update(category);
        await _db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Product category has been deleted.";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = true;
        response.Message = "Error has been occured while deleting the data. Please try again.";
      }

      return response;
    }

    public DownloadFileViewModel DownloadProductCategoryImage(int id)
    {
      throw new NotImplementedException();
    }

    public List<ProductCategoryViewModel> GetAllProductCategories()
    {
      var query = _db.ProductCategories.Where(t => t.IsActive == true).OrderBy(t => t.Name);


      var data = new List<ProductCategoryViewModel>();



      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm(_config));
      });




      return data;
    }

    public ProductCategoryViewModel GetProductCategoryById(long id)
    {
      var response = new ProductCategoryViewModel();

      var pCategory = _db.ProductCategories.FirstOrDefault(x => x.Id == id);

      response.Description = pCategory.Description;
      response.Id = pCategory.Id;
      response.IsActive = pCategory.IsActive.Value;
      response.Name = pCategory.Name;
      response.Picture = pCategory.Picture;

      return response;
    }

    public async Task<ResponseViewModel> SaveProductCategory(ProductCategoryViewModel vm, string userName)
    {
      var response = new ResponseViewModel();

      try
      {

        var category = _db.ProductCategories.FirstOrDefault(d => d.Id == vm.Id);

        if (category == null)
        {
          category = new Model.ProductCategory()
          {
            Name = vm.Name,
            Description = vm.Description,
            IsActive = true,
            Picture = vm.Picture
          };

          _db.ProductCategories.Add(category);
          await _db.SaveChangesAsync();

          response.IsSuccess = true;
          response.Message = "New Product category has been added.";
        }
        else
        {
          category.Description = vm.Description;
          category.Name = vm.Name;
          category.Picture = vm.Picture;

          _db.ProductCategories.Update(category);
          await _db.SaveChangesAsync();

          response.IsSuccess = true;
          response.Message = "Product category has been updated.";
        }
      }
      catch (Exception ex)
      {

        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the data. Please try again.";
      }

      return response;
    }

    public async Task<ResponseViewModel> UploadProductCategoryImage(FileContainerModel container, string userName)
    {
      var response = new ResponseViewModel();


      try
      {
        var user = _db.Users.FirstOrDefault(t => t.Username == userName);

        var productCategoryRecord = _db.ProductCategories.FirstOrDefault(x => x.Id == container.Id);

        var folderPath = productCategoryRecord.GetProductCategoryImageFolderPath(_config);

        if (!string.IsNullOrEmpty(productCategoryRecord.Picture))
        {
          var existingImagePath = string.Format(@"{0}\{1}", folderPath, productCategoryRecord.Picture);

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
          var fileName = productCategoryRecord.GetProductCategoryImageName(Path.GetExtension(firstFile.FileName));
          var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await firstFile.CopyToAsync(stream);

            productCategoryRecord.Picture = fileName;

            _db.ProductCategories.Update(productCategoryRecord);

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
  }
}
