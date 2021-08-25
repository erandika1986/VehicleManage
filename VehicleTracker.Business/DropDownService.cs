using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business
{
  public class DropDownService : IDropDownService
  {

    #region Member variable

    private readonly VMDBContext _db;
    private readonly IConfiguration _config;
    private readonly ILogger<IDropDownService> _logger;

    #endregion

    public DropDownService(VMDBContext db, IConfiguration config, ILogger<IDropDownService> logger)
    {
      this._db = db;
      this._config = config;
      this._logger = logger;
    }
    public List<DropDownViewModal> GetProductSubCategories(int categoryId)
    {
      var productCategories = _db.ProductSubCategories
        .Where(x => x.IsActive == true && x.ProductCategoryId == categoryId)
        .Select(c => new DropDownViewModal() { Id = c.Id, Name = c.Name }).ToList();


      return productCategories;
    }


    public List<DropDownViewModal> GetProducts(int subCategoryId)
    {
      var productCategories = _db.Products
        .Where(x => x.IsActive == true && x.SubProductCategoryId == subCategoryId)
        .Select(c => new DropDownViewModal() { Id = c.Id, Name = c.ProductName }).ToList();


      return productCategories;
    }

    public List<DropDownViewModal> GetProductsForSupplier(int subCategoryId, int supplierId)
    {
      var productCategories = _db.Products
        .Where(x => x.IsActive == true && x.SubProductCategoryId == subCategoryId && x.SupplierId == supplierId)
        .Select(c => new DropDownViewModal() { Id = c.Id, Name = c.ProductName }).ToList();


      return productCategories;
    }
  }
}
