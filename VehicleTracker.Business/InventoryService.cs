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

          if(productInventory==null)
          {
            productInventory = new ProductInventory()
            {
              DateRecieved = item.DateRecieved,
              BatchNo = item.BatchNo,
              DateOfManufacture = item.DateOfManufacture,
              DateOfExpiration = item.DateOfExpiration,
              ProductId = item.ProductId,
              WarehouseId = pOInventoryReceievedDetail.WarehouseId,
              RecievedQty = item.RecievedQty,
              AvailableQty = item.RecievedQty,
              Action = 0,
              IsActive = true,
              PurchaseOrderId = pOInventoryReceievedDetail.PuchaseOrderId,
              UdatedById = user.Id,
              UpdatedOn = DateTime.UtcNow,
              CreatedById = user.Id,
              CreatedOn = DateTime.UtcNow
            };

            productInventory.ProductInventoryReceivedHistories = new HashSet<ProductInventoryReceivedHistory>();

            var productInventoryReceived = new ProductInventoryReceivedHistory()
            {
              ReceivedQty = item.RecievedQty,
              ReceivedDate = item.DateRecieved,
              CreatedOn = DateTime.UtcNow,
              CreatedById = user.Id,
              UpdatedOn = DateTime.UtcNow,
              UpdatedById = user.Id
            };

            productInventory.ProductInventoryReceivedHistories.Add(productInventoryReceived);

            _db.ProductInventories.Add(productInventory);
          }
          else
          {
            productInventory.AvailableQty = productInventory.AvailableQty + item.AvailableQty;
            productInventory.IsActive = true;
            productInventory.UdatedById = user.Id;
            productInventory.UpdatedOn = DateTime.UtcNow;

            var productInventoryReceived = new ProductInventoryReceivedHistory()
            {
              ReceivedQty = item.RecievedQty,
              ReceivedDate = item.DateRecieved,
              CreatedOn = DateTime.UtcNow,
              CreatedById = user.Id,
              UpdatedOn = DateTime.UtcNow,
              UpdatedById = user.Id
            };

            productInventory.ProductInventoryReceivedHistories.Add(productInventoryReceived);
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

    public async Task<ResponseViewModel> DeleteInventory(int id)
    {
      var response = new ResponseViewModel();

      try
      {
        var productInventory = _db.ProductInventories.FirstOrDefault(x => x.Id == id && x.IsActive == true);

        if(productInventory.ProductInventoryOrders.Count()==0)
        {
          foreach (var item in productInventory.ProductInventoryReceivedHistories)
          {
            _db.ProductInventoryReceivedHistories.Remove(item);
          }
          _db.ProductInventories.Remove(productInventory);

          await _db.SaveChangesAsync();

          response.IsSuccess = true;
          response.Message = "Product Inventory deleted successfully.";
        }
        else
        {
          response.IsSuccess = false;
          response.Message = "Unable to delete the inventory record since it is already in use with sales order";
        }
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
        var inventoryBasicDetail = new InventoryBasicDetail()
        {
          CategoryName = item.SubProductCategory.ProductCategory.Name,
          ProductId= item.Id,
          ProductName=item.ProductName,
          SubCategoryName=item.SubProductCategory.Name,
          SupplierName=item.Supplier.Name,
          QtyInHand = item.ProductInventories.Where(p=>p.IsActive==true).Sum(x=>x.AvailableQty),
          TotalItemRecieved = item.ProductInventories.SelectMany(x=>x.ProductInventoryReceivedHistories).Sum(x=>x.ReceivedQty),
          TotalItemReturn = item.ProductReturns.Sum(x=>x.Qty)
        };

        response.Add(inventoryBasicDetail);
      }

      return response;
    }

    public async Task<ResponseViewModel> UpdateInvetoryRecord(InventoryViewModel vm)
    {
      var response = new ResponseViewModel();

      try
      {

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while updating inventory records.";
      }

      return response;
    }
  }
}
