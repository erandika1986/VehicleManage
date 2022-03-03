using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.Model.Enums;
using VehicleTracker.Report;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.PurchaseOrder;

namespace VehicleTracker.Business
{
  public class PurchaseOrderService : IPurchaseOrderService
  {
    #region Member variable

    private readonly VMDBContext _db;
    private readonly IConfiguration _config;
    private readonly IUserService _userService;
    private readonly ILogger<IPurchaseOrderService> _logger;

    #endregion

    public PurchaseOrderService(VMDBContext db, IConfiguration config, IUserService userService, ILogger<IPurchaseOrderService> logger)
    {
      this._db = db;
      this._config = config;
      this._userService = userService;
      this._logger = logger;
    }

    public async Task<ResponseViewModel> SavePurchaseOrder(PurchaseOrderViewModel vm, string userName)
    {
      var response = new ResponseViewModel();
      try
      {
        var po = _db.PurchaseOrders.FirstOrDefault(p => p.Id == vm.Id);
        var user = _db.Users.FirstOrDefault(t => t.Username == userName);

        if (po == null)
        {

          po = new PurchaseOrder()
          {
            Ponumber = vm.PONumber,
            SupplierId = vm.SelectedSupplierId,
            ShippedToWharehouseId = vm.SelectedWarehouseId,
            Date=vm.Date,
            Remark = vm.Remarks,
            SubTotal = vm.SubTotal,
            Discount = vm.Discount,
            TaxRate = vm.TaxRate,
            TotalTaxAmount = vm.TotalTaxAmout,
            ShipingCharge = vm.ShippingCharge,
            Total = vm.Total,
            Status = (int)vm.Status,
            CreatedOn = DateTime.UtcNow,
            CreatedById = user.Id,
            UpdatedOn = DateTime.UtcNow,
            UpdatedById = user.Id,
            IsActive = true,
          };

          po.PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
          AddNewPOItems(po, vm.Items);

          _db.PurchaseOrders.Add(po);
          response.Message = "New purchase order has been saved successfully.!";
        }
        else
        {
          po.SupplierId = vm.SelectedSupplierId;
          po.ShippedToWharehouseId = vm.SelectedWarehouseId;
          po.Remark = vm.Remarks;
          po.Date = vm.Date;
          po.SubTotal = vm.SubTotal;
          po.Discount = vm.Discount;
          po.TaxRate = vm.TaxRate;
          po.TotalTaxAmount = vm.TotalTaxAmout;
          po.ShipingCharge = vm.ShippingCharge;
          po.Total = vm.Total;
          po.Status = (int)vm.Status;
          po.UpdatedOn = DateTime.UtcNow;
          po.UpdatedById = user.Id;

          var newlyAddedItems = vm.Items.Where(x => x.Id == 0).ToList();
          AddNewPOItems(po, newlyAddedItems);

          var updatedPoItems = (from p in vm.Items where po.PurchaseOrderDetails.Any(x => x.Id == p.Id) select p).ToList();
          foreach (var item in updatedPoItems)
          {
            var poDetail = po.PurchaseOrderDetails.FirstOrDefault(p => p.Id == item.Id);
            poDetail.ProductId = item.ProductId;
            poDetail.Qty = item.Qty;
            poDetail.UnitPrice = item.UnitPrice;
            poDetail.Total = item.Qty * item.UnitPrice;
          }

          var deletedPoItems = (from d in po.PurchaseOrderDetails where !vm.Items.Any(x => x.Id == d.Id) select d).ToList();

          foreach (var item in deletedPoItems)
          {
            po.PurchaseOrderDetails.Remove(item);
          }

          _db.PurchaseOrders.Update(po);
          response.Message = "Purchase order has been updated successfully.!";
        }

        await _db.SaveChangesAsync();
        response.IsSuccess = true;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while saving the Purchase order data. Please try again.";
      }

      return response;
    }

    public async Task<ResponseViewModel> DeletePurchaseOrder(int id, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var po = _db.PurchaseOrders.FirstOrDefault(p => p.Id == id);
        var user = _db.Users.FirstOrDefault(t => t.Username == userName);

        po.IsActive = false;
        po.Status = (int)POStatus.Canceled;
        po.UpdatedOn = DateTime.UtcNow;
        po.UpdatedById = user.Id;

        _db.PurchaseOrders.Update(po);
        await _db.SaveChangesAsync();

        response.IsSuccess = true;
        response.Message = "Purchase order has been deleted successfully.!";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured while deleting the purchase order. Please try again.";
      }

      return response;
    }

    public List<PurchaseOrderSummaryViewModel> GetAllPurchseOrder(PurchaseOrderFilter filter, string username)
    {
      var response = new List<PurchaseOrderSummaryViewModel>();
      var user = _db.Users.FirstOrDefault(t => t.Username == username);
      var pos = _db.PurchaseOrders.OrderByDescending(x => x.UpdatedOn);

      if(filter.SelectedStatusId>0)
      {
        pos = pos.Where(x=>x.Status==filter.SelectedStatusId).OrderByDescending(x => x.UpdatedOn);
      }

      if (filter.SelectedSupplierId > 0)
      {
        pos = pos.Where(x => x.Status == filter.SelectedSupplierId).OrderByDescending(x => x.UpdatedOn);
      }

      if (filter.SelectedWarehouseNameId > 0)
      {
        pos = pos.Where(x => x.Status == filter.SelectedWarehouseNameId).OrderByDescending(x => x.UpdatedOn);
      }

      foreach (var item in pos)
      {
        var summary = new PurchaseOrderSummaryViewModel()
        {
          Id = item.Id,
          Discount = item.Discount,
          PONumber = item.Ponumber,
          ShippingCharges = item.ShipingCharge,
          Status = ((POStatus)item.Status).ToString(),
          SubTotal = item.SubTotal,
          SupplierName = item.Supplier.Name,
          TaxRate = item.TaxRate,
          Total = item.Total,
          TotalTaxAmount = item.TotalTaxAmount,
          WarehouseName = item.ShippedToWharehouse.Name,
          Date = item.Date.ConvertToUserTime(user.TimeZone.TimeZoneId).ToString("MM/dd/yyyy hh:mm tt") 
      };

        response.Add(summary);
      }

      return response;
    }

    public PurchaseOrderViewModel GetPurchaseOrderById(int id, string username)
    {
      var response = new PurchaseOrderViewModel();

      var user = _db.Users.FirstOrDefault(t => t.Username == username);

      var po = _db.PurchaseOrders.FirstOrDefault(p => p.Id == id);

      response.Discount = po.Discount;
      response.Id = po.Id;
      response.PONumber = po.Ponumber;
      response.Remarks = po.Remark;
      response.SelectedSupplierId = po.SupplierId;
      response.SelectedWarehouseId = po.ShippedToWharehouseId;
      response.ShippingCharge = po.ShipingCharge;
      response.Status = (POStatus)po.Status;
      response.SubTotal = po.SubTotal;
      response.TaxRate = po.TaxRate;
      response.Total = po.Total;
      response.Date = po.Date;
      response.TotalTaxAmout = po.TotalTaxAmount;
      response.CreatedOn = po.CreatedOn.ConvertToUserTime(user.TimeZone.TimeZoneId).ToString("MM/dd/yyyy hh:mm tt");
      response.CreatedBy = $"{po.CreatedBy.FirstName} {po.CreatedBy.LastName}";
      response.UpdatedOn = po.UpdatedOn.ConvertToUserTime(user.TimeZone.TimeZoneId).ToString("MM/dd/yyyy hh:mm tt");
      response.UpdatedBy = $"{po.UpdatedBy.FirstName} {po.UpdatedBy.LastName}";

      foreach (var item in po.PurchaseOrderDetails)
      {
        var poItem = new PurchaseOrderItemViewModel()
        {
          Id = item.Id,
          ProductId = item.ProductId,
          SelectedCategoryId = item.Product.SubProductCategory.ProductCategoryId,
          SelectedSubCategoryId = item.Product.SubProductCategoryId,
          PurchaseOrderId = item.PurchaseOrderId,
          Qty = item.Qty,
          Total = item.Total,
          UnitPrice = item.UnitPrice
        };

        poItem.Categories.AddRange(GetProductCategories());
        poItem.SubCategories.AddRange(GetProductSubCategories(item.Product.SubProductCategory.ProductCategoryId));
        poItem.Products.AddRange(GetProducts(item.Product.SubProductCategoryId, po.SupplierId));

        response.Items.Add(poItem);
      }

      return response;
    }

    public PurchaseOrderMasterData GetPurchaseOrderMasterData()
    {
      var response = new PurchaseOrderMasterData();

      var suppliers = _db.Suppliers.Where(x => x.IsActive == true).ToList();
      foreach (var item in suppliers)
      {
        response.Suppliers.Add(new DropDownViewModel() { Id = item.Id, Name = item.Name });
      }

      var warehouses = _db.Wharehouses.Where(x => x.IsActive == true).ToList();
      foreach (var item in warehouses)
      {
        response.Warehouses.Add(new DropDownViewModel() { Id = item.Id, Name = item.Name });
      }

      response.ProductCategories.AddRange(GetProductCategories());


      foreach (POStatus value in Enum.GetValues(typeof(POStatus)))
      {
        response.Statuses.Add(new DropDownViewModel() { Id = (int)value, Name = EnumHelper.GetEnumDescription(value) });
      }

      return response;
    }

    public async Task<PONumber> GetPONumber()
    {
      var po = new PONumber()
      {
        Number = await GeneratePONumber()
      };
      return po;
    }

    public List<DropDownViewModel> GetProductSubCategories(int categoryId)
    {
      var productCategories = _db.ProductSubCategories
        .Where(x => x.IsActive == true && x.ProductCategoryId == categoryId)
        .Select(c => new DropDownViewModel() { Id = c.Id, Name = c.Name }).ToList();


      return productCategories;
    }

    public List<DropDownViewModel> GetProducts(int subCategoryId,int supplierId)
    {
      var productCategories = _db.Products
        .Where(x => x.IsActive == true && x.SubProductCategoryId == subCategoryId && x.SupplierId==supplierId)
        .Select(c => new DropDownViewModel() { Id = c.Id, Name = c.ProductName }).ToList();


      return productCategories;
    }

    public DownloadFileViewModel DownloadPurchasingOrderForm(int id)
    {
      var reportFacotry = new ReportFactory();
      var reportParams = new Dictionary<string, string>();

      reportParams.Add("id", id.ToString());

      reportParams.Add("ReportType", "PO");

      var poReportGenerator = reportFacotry.GetPDFGenerator(reportParams, _db, _config);

      var response = poReportGenerator.GeneratePDFReport();

      return response;
  }

    private void AddNewPOItems(PurchaseOrder po, List<PurchaseOrderItemViewModel> items)
    {
      foreach (var item in items)
      {
        var poDetail = new PurchaseOrderDetail()
        {
          ProductId = item.ProductId,
          Qty = item.Qty,
          Total = item.Qty * item.UnitPrice,
          UnitPrice = item.UnitPrice,
        };

        po.PurchaseOrderDetails.Add(poDetail);
      }
    }

    private async Task<string> GeneratePONumber()
    {
      string newPO = string.Empty;
      var currentPO = _db.AppSettings.FirstOrDefault(x => x.Key == "PONumber");

      if (string.IsNullOrEmpty(currentPO.Value))
      {
        currentPO.Value = "000001";
        _db.AppSettings.Update(currentPO);
        await _db.SaveChangesAsync();
      }
      else
      {
        var value = int.Parse(currentPO.Value);
        value++;
        currentPO.Value = value.ToString().PadLeft(6, '0');
        _db.AppSettings.Update(currentPO);
        await _db.SaveChangesAsync();
      }

      newPO = $"PO-{currentPO.Value}";
      return newPO;
    }

    private List<DropDownViewModel> GetProductCategories()
    {
      var productCategories = _db.ProductCategories
        .Where(x => x.IsActive == true)
        .Select(c=>  new DropDownViewModel() { Id = c.Id, Name = c.Name }).ToList();


      return productCategories;
    }

  }

  public class PONumber
  {
    public string Number { get; set; }
  }
}
