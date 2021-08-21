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
using VehicleTracker.ViewModel.SalesOrder;

namespace VehicleTracker.Business
{
  public class SaleOrderService : ISaleOrderService
  {
    #region Member variable
    private readonly VMDBContext _db;
    private readonly IUserService _userService;
    private readonly ILogger<ISaleOrderService> _logger;

    #endregion

    public SaleOrderService(VMDBContext db, IUserService userService, ILogger<ISaleOrderService> logger)
    {
      this._db = db;
      this._userService = userService;
      this._logger = logger;
    }


    public async Task<ResponseViewModel> DeleteSalesOrder(int id, User loggedInUser)
    {
      var response = new ResponseViewModel();

      try
      {
        var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == id);

        salesOrder.Status = (int)VehicleTracker.Model.Enums.SalesOrderStatus.Cancelled;
        salesOrder.IsActive = false;
        salesOrder.UpdatedById = loggedInUser.Id;
        salesOrder.UpdatedOn = DateTime.UtcNow;

        _db.SalesOrders.Update(salesOrder);

        await _db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Selected sales order has been deleted successfully.";
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while deleting the sales order.";
      }

      return response;
    }

    public List<BasicSalesOrderDetailViewModel> GetAllSalesOrders(SalesOrderFilter filters)
    {
      var response = new List<BasicSalesOrderDetailViewModel>();

      var allSalesOrders = _db.SalesOrders.Where(x => x.IsActive == true).OrderByDescending(s=>s.CreatedOn);

      if(filters.SelectedCustomerId>0)
      {
        allSalesOrders= allSalesOrders.Where(x => x.OwnerId==filters.SelectedCustomerId).OrderByDescending(s => s.CreatedOn);
      }

      if (filters.SelectedSalesPersonId > 0)
      {
        allSalesOrders = allSalesOrders.Where(x => x.CreatedById == filters.SelectedSalesPersonId).OrderByDescending(s => s.CreatedOn);
      }

      if (filters.SelectedStatus > 0)
      {
        allSalesOrders = allSalesOrders.Where(x => x.Status == filters.SelectedStatus).OrderByDescending(s => s.CreatedOn);
      }

      var salesOrderList = allSalesOrders.ToList();

      foreach (var item in salesOrderList)
      {
        var salesOrder = new BasicSalesOrderDetailViewModel()
        {
          CreatedBy=string.Format("{0} {1}",item.CreatedBy.FirstName,item.CreatedBy.LastName),
          CreatedOn= item.CreatedOn.ToString("MM/dd/yyyy"),
          OrderDate= item.OrderDate.ToString("MM/dd/yyyy"),
          OrderNumber = item.OrderNumber,
          OwnerAddress= item.Owner.Address,
          OwnderName=item.Owner.Name,
          Route = item.Owner.Route.Name,
          Status= item.StatusNavigation.Name,
          Id=item.Id,
          Total = item.TotalAmount,
          TotalQty = item.SalesOrderItems.Sum(x=>x.Qty),
          UpdatedBy = string.Format("{0} {1}", item.UpdatedBy.FirstName, item.UpdatedBy.LastName),
          UpdatedOn = item.UpdatedOn.ToString("MM/dd/yyyy")

        };

        response.Add(salesOrder);
      }

      return response;
    }

    public List<BasicSalesOrderDetailViewModel> GetMySalesOrders(SalesOrderFilter filters, User loggedInUser)
    {
      var response = new List<BasicSalesOrderDetailViewModel>();

      var allSalesOrders = _db.SalesOrders.Where(x => x.IsActive == true && x.OwnerId==loggedInUser.Id).OrderByDescending(s => s.CreatedOn);

      if (filters.SelectedCustomerId > 0)
      {
        allSalesOrders = allSalesOrders.Where(x => x.OwnerId == filters.SelectedCustomerId).OrderByDescending(s => s.CreatedOn);
      }

      if (filters.SelectedSalesPersonId > 0)
      {
        allSalesOrders = allSalesOrders.Where(x => x.CreatedById == filters.SelectedSalesPersonId).OrderByDescending(s => s.CreatedOn);
      }

      if (filters.SelectedStatus > 0)
      {
        allSalesOrders = allSalesOrders.Where(x => x.Status == filters.SelectedStatus).OrderByDescending(s => s.CreatedOn);
      }

      var salesOrderList = allSalesOrders.ToList();

      foreach (var item in salesOrderList)
      {
        var salesOrder = new BasicSalesOrderDetailViewModel()
        {
          CreatedBy = string.Format("{0} {1}", item.CreatedBy.FirstName, item.CreatedBy.LastName),
          CreatedOn = item.CreatedOn.ToString("MM/dd/yyyy"),
          OrderDate = item.OrderDate.ToString("MM/dd/yyyy"),
          OrderNumber = item.OrderNumber,
          OwnerAddress = item.Owner.Address,
          OwnderName = item.Owner.Name,
          Route = item.Owner.Route.Name,
          Status = item.StatusNavigation.Name,
          Id = item.Id,
          Total = item.TotalAmount,
          TotalQty = item.SalesOrderItems.Sum(x => x.Qty),
          UpdatedBy = string.Format("{0} {1}", item.UpdatedBy.FirstName, item.UpdatedBy.LastName),
          UpdatedOn = item.UpdatedOn.ToString("MM/dd/yyyy")

        };

        response.Add(salesOrder);
      }

      return response;
    }

    public async Task<ResponseViewModel> SaveSalesOrder(SalesOrderViewModel vm, User loggedInUser)
    {
      var response = new ResponseViewModel();

      try
      {
        var salesOrder = _db.SalesOrders.FirstOrDefault(x => x.Id == vm.Id);
        if(salesOrder==null)
        {
          salesOrder.IsActive = true;
          salesOrder.CreatedById = loggedInUser.Id;
          salesOrder.CreatedOn = DateTime.UtcNow;
          salesOrder.Discount = vm.Discount;
          salesOrder.OrderDate = vm.OrderDate;
          salesOrder.OrderNumber = vm.OrderNumber;
          salesOrder.OwnerId = vm.OwnerId;
          salesOrder.ShippingCharge = vm.ShippingCharge;
          salesOrder.Status = vm.Status;
          salesOrder.SubTotal = vm.SubTotal;
          salesOrder.TaxRate = vm.TaxRate;
          salesOrder.TotalTaxAmount = vm.TotalTaxAmount;
          salesOrder.TotalAmount = vm.TotalAmount;
          salesOrder.UpdatedById = loggedInUser.Id;
          salesOrder.UpdatedOn = DateTime.UtcNow;

          salesOrder.SalesOrderItems = new HashSet<SalesOrderItem>();

          AddNewSalesOrderItems(salesOrder, vm.Items);

          _db.SalesOrders.Add(salesOrder);

          response.Message = "New sales order has been added successfully.";
        }
        else
        {
          salesOrder.Discount = vm.Discount;
          salesOrder.OrderDate = vm.OrderDate;
          salesOrder.OwnerId = vm.OwnerId;
          salesOrder.ShippingCharge = vm.ShippingCharge;
          salesOrder.Status = vm.Status;
          salesOrder.SubTotal = vm.SubTotal;
          salesOrder.TaxRate = vm.TaxRate;
          salesOrder.TotalTaxAmount = vm.TotalTaxAmount;
          salesOrder.TotalAmount = vm.TotalAmount;
          salesOrder.UpdatedById = loggedInUser.Id;
          salesOrder.UpdatedOn = DateTime.UtcNow;

          var existingItems = salesOrder.SalesOrderItems.ToList();

          //Add newly added sales order items
          var newlyAddedItems = (from u in vm.Items where !existingItems.Any(x => x.Id == u.Id) select u).ToList();

          AddNewSalesOrderItems(salesOrder, newlyAddedItems);

          //Updated existing order items
          var updateItems = (from u in vm.Items where existingItems.Any(x => x.Id == u.Id) select u).ToList();

          foreach (var item in updateItems)
          {
            var existingSalesOrderItem = existingItems.FirstOrDefault(x => x.Id == item.Id);

            existingSalesOrderItem.ProductId = item.ProductId;
            existingSalesOrderItem.Qty = item.Qty;
            existingSalesOrderItem.Total = item.Total;
            existingSalesOrderItem.UnitPrice = item.UnitPrice;

            _db.SalesOrderItems.Update(existingSalesOrderItem);
          }

          //Delete deleted items
          var deletedItems = (from d in existingItems where !vm.Items.Any(x => x.Id == d.Id) select d).ToList();
          foreach (var item in deletedItems)
          {
            _db.SalesOrderItems.Remove(item);
          }

          response.Message = string.Format("Existing sales order ({0}) has been updated successfully.",salesOrder.OrderNumber);
        }

        await _db.SaveChangesAsync();
        response.IsSuccess = true;
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the sales order.";
      }

      return response;
    }

    private void AddNewSalesOrderItems(SalesOrder salesOrder,List<SalesOrderItemViewModel> items)
    {
      foreach (var item in items)
      {
        var sitem = new SalesOrderItem()
        {
          ProductId = item.ProductId,
          Qty = item.Qty,
          UnitPrice = item.UnitPrice,
          Total = item.Qty * item.UnitPrice
        };

        salesOrder.SalesOrderItems.Add(sitem);
      }
    }
  }
}
