using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business
{
  public class ProductService : IProductService
  {
    #region Member variable

    private readonly VMDBContext _db;
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    #endregion

    public ProductService(VMDBContext db, IUserService userService, IConfiguration config)
    {
      this._db = db;
      this._userService = userService;
      this._config = config;
    }

    public async Task<ResponseViewModel> DeleteProduct(int id, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var user = _userService.GetUserByUsername(userName);

        var product = _db.Products.FirstOrDefault(x => x.Id == id);
        product.IsActive = false;
        product.UpdatedOn = DateTime.UtcNow;
        product.UpdatedById = user.Id;

        _db.Products.Update(product);

        await _db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Product has deleted successfully.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Product deletion has been failed. Please try again";
      }

      return response;


    }

    public List<ProductViewModel> GetAllProducts(int productSubCategryId)
    {
      var query = _db.Products.OrderBy(t => t.ProductName);

      if (productSubCategryId > 0)
      {
        query = query.Where(t => t.SubProductCategoryId == productSubCategryId).OrderBy(t => t.ProductName);
      }

      var data = new List<ProductViewModel>();

      var pageData = query.ToList();

      pageData.ForEach(p =>
      {
        data.Add(p.ToVm(_config));
      });

      return data;
    }

    public ProductViewModel GetProductById(int id)
    {
      var product = _db.Products.FirstOrDefault(x => x.Id == id);

      return product.ToVm(_config);
    }

    public List<DropDownViewModal> GetProductSubCategories(int categoryId)
    {
      var response = _db.ProductSubCategories
                      .Where(p => p.ProductCategoryId == categoryId && p.IsActive == true)
                      .OrderBy(x => x.Name)
                      .Select(t => new DropDownViewModal() { Id = t.Id, Name = t.Name }).ToList();

      return response;
    }

    public async Task<ResponseViewModel> SaveProduct(ProductViewModel vm, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var user = _userService.GetUserByUsername(userName);

        var product = _db.Products.FirstOrDefault(x => x.Id == vm.Id);

        if (product == null)
        {
          product = vm.ToModel();
          product.CreatedOn = DateTime.UtcNow;
          product.CreatedById = user.Id;
          product.UpdatedOn = DateTime.UtcNow;
          product.UpdatedById = user.Id;

          _db.Products.Add(product);
          await _db.SaveChangesAsync();

        }
        else
        {
          product.ProductName = vm.Name;
          product.IsActive = vm.IsActive;
          product.ProductCode = vm.ProductCode;
          product.SubProductCategoryId = vm.ProductSubCategoryId;
          product.SubProductCategoryId = vm.ProductSubCategoryId;
          product.SupplierId = vm.SupplierId;
          product.UnitPrice = vm.UnitPrice;
          product.UpdatedOn = DateTime.UtcNow;
          product.UpdatedById = user.Id;

          _db.Products.Update(product);

          await _db.SaveChangesAsync();
        }


        response.IsSuccess = true;
        response.Message = "Product has deleted successfully.";
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = "Product deletion has been failed. Please try again";
      }

      return response;
    }
  }
}
