using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Inventory;

namespace VehicleTracker.Business
{
  public class InventoryService: IInventoryService
  {
    #region Member variable

    private readonly VMDBContext _db;
    private readonly IConfiguration _config;
    private readonly IUserService _userService;
    private readonly ILogger<IInventoryService> _logger;

    #endregion

    public InventoryService(VMDBContext db, IConfiguration config, IUserService userService, ILogger<IInventoryService> logger)
    {
      this._db = db;
      this._config = config;
      this._userService = userService;
      this._logger = logger;
    }

    public async Task<ResponseViewModel> AddNewInventoryRecords(POInventoryReceievedDetail pOInventoryReceievedDetail, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var user = _userService.GetUserByUsername(userName);

        foreach (var item in pOInventoryReceievedDetail.Inventories)
        {

          var productInventory = _db.ProductInventories.FirstOrDefault(x => x.Id == item.Id);

          if (productInventory == null)
          {
            productInventory = new ProductInventory()
            {
              ProductId = item.ProductId,
              WarehouseId = pOInventoryReceievedDetail.WarehouseId,
              ReceivedQty = item.RecievedQty,
              BatchNo = item.BatchNo,
              DateOfManufacture = item.DateOfManufacture,
              DateOfExpiration = item.DateOfExpiration,
              Action = 0,
              IsActive = true,
              PurchaseOrderId = pOInventoryReceievedDetail.PuchaseOrderId,
              UdatedById = user.Id,
              UpdatedOn = DateTime.UtcNow,
              CreatedById = user.Id,
              CreatedOn = DateTime.UtcNow
            };

            _db.ProductInventories.Add(productInventory);
          }
          else
          {
            productInventory.WarehouseId = pOInventoryReceievedDetail.WarehouseId;
            productInventory.ReceivedQty = item.RecievedQty;
            productInventory.BatchNo = item.BatchNo;
            productInventory.DateOfManufacture = item.DateOfManufacture;
            productInventory.DateOfExpiration = item.DateOfExpiration;
            productInventory.UdatedById = user.Id;
            productInventory.UpdatedOn = DateTime.UtcNow;


            _db.ProductInventories.Update(productInventory);

          }
        }

        await _db.SaveChangesAsync();
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while adding new inventory records.";
      }

      return response;
    }

    public POInventoryReceievedDetail GetInventoryDetailsForPO(int poId)
    {
      var response = new POInventoryReceievedDetail();

      var po = _db.PurchaseOrders.FirstOrDefault(x => x.Id == poId);

      response.PuchaseOrderId = poId;

      response.WarehouseId = po.ShippedToWharehouseId;

      if(po.ProductInventories.Count()>0)
      {

        var poProducts = po.PurchaseOrderDetails.GroupBy(x => x.ProductId).Select(p=> new { ProductId= p.Key, ProductList = p.ToList()}).ToList();

        foreach (var item in poProducts)
        {
          var inventoryItem = new InventoryViewModel()
          {
            SupplierName = item.ProductList.FirstOrDefault().Product.Supplier.Name,
            ProductName = item.ProductList.FirstOrDefault().Product.ProductName,
            ProductCategoryName= item.ProductList.FirstOrDefault().Product.SubProductCategory.ProductCategory.Name,
            ProductSubCategoryName=item.ProductList.FirstOrDefault().Product.SubProductCategory.Name,
            Id=0,
            IsActive=true,
            ProductId=item.ProductId,
            RecievedQty=0,
            TotalOrderedQty=  item.ProductList.Sum(x=>x.Qty)
          };

          response.Inventories.Add(inventoryItem);
        }
      }
      else
      {
        var existingInventories = po.ProductInventories.OrderBy(x => x.Product.ProductName).ToList();

        foreach (var item in existingInventories)
        {
          var inventoryItem = new InventoryViewModel()
          {
            SupplierName = item.Product.Supplier.Name,
            ProductName = item.Product.ProductName,
            ProductCategoryName = item.Product.SubProductCategory.ProductCategory.Name,
            ProductSubCategoryName = item.Product.SubProductCategory.Name,
            Id = item.Id,
            IsActive = true,
            ProductId = item.ProductId,
            RecievedQty = item.ReceivedQty,
            TotalOrderedQty = po.PurchaseOrderDetails.Where(p=>p.ProductId==item.ProductId).Sum(x=>x.Qty)
          };

          response.Inventories.Add(inventoryItem);
        }
      }

      return response;
    }

    public async Task<ResponseViewModel> DeleteInventory(int id)
    {
      var response = new ResponseViewModel();

      try
      {
        var productInventory = _db.ProductInventories.FirstOrDefault(x => x.Id == id && x.IsActive == true);

        var otherInventoryRecords = _db.ProductInventories.Where(x => x.ProductId == productInventory.ProductId && x.PurchaseOrderId == productInventory.PurchaseOrderId && x.Id != productInventory.Id).ToList();

        if(otherInventoryRecords.Count>0)
        {
          _db.ProductInventories.Remove(productInventory);
        }
        else
        {
          productInventory.ReceivedQty = 0;
          _db.ProductInventories.Update(productInventory);
        }
          await _db.SaveChangesAsync();

          response.IsSuccess = true;
          response.Message = "Product Inventory deleted successfully.";

        response.IsSuccess = false;
        response.Message = "Unable to delete the inventory record since it is already in use with sales order";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while deleting inventory records.";
      }

      return response;
    }

    public List<InventoryBasicDetail> GetProductInvetorySummary()
    {
      var response = new List<InventoryBasicDetail>();

      var products = _db.Products.Where(x => x.IsActive == true).OrderBy(x => x.ProductName);

      foreach (var item in products)
      {
        var totalItemReceived = item.ProductInventories.Where(x => x.ProductId == item.Id).Sum(x => x.ReceivedQty);

        var qtyInHand = totalItemReceived - item.ProductInventoryOrders.Where(x => x.ProductId == item.Id).Sum(x => x.Qty);

        var inventoryBasicDetail = new InventoryBasicDetail()
        {
          ProductImage = item.GetProductDefaultThumnialImage(_config),
          CategoryName = item.SubProductCategory.ProductCategory.Name,
          ProductId= item.Id,
          ProductName=item.ProductName,
          SubCategoryName=item.SubProductCategory.Name,
          SupplierName=item.Supplier.Name,
          TotalItemRecieved = totalItemReceived,
          QtyInHand = qtyInHand,
          TotalItemReturn = item.ProductReturns.Sum(x=>x.Qty)
        };

        response.Add(inventoryBasicDetail);
      }

      return response;
    }

    public InventoryMasterDataViewModel GetInventoryMasterData()
    {
      var response = new InventoryMasterDataViewModel();

      var suppliers = _db.Suppliers.Where(x => x.IsActive == true).ToList();
      foreach (var item in suppliers)
      {
        response.Suppliers.Add(new DropDownViewModal() { Id = item.Id, Name = item.Name });
      }

      var warehouses = _db.Wharehouses.Where(x => x.IsActive == true).ToList();
      foreach (var item in warehouses)
      {
        response.Warehouses.Add(new DropDownViewModal() { Id = item.Id, Name = item.Name });
      }

      response.ProductCategories.AddRange(_db.ProductCategories
        .Where(x => x.IsActive == true)
        .Select(c => new DropDownViewModal() { Id = c.Id, Name = c.Name }).ToList());

      return response;
    }
  }
}
