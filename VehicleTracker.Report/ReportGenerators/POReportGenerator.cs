using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Data;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.PurchaseOrder;

namespace VehicleTracker.Report
{
  public class POReportGenerator: BaseReportGenerator
  {
    public POReportGenerator(Dictionary<string, string> reportParams, VMDBContext db)
      : base(reportParams, db)
    {
      
    }

    public override FileViewModel GenerateExcelReport()
    {
      return base.GenerateExcelReport();
    }

    public override FileViewModel GeneratePDFReport()
    {
      return base.GeneratePDFReport();
    }


    private PurshaseOrderReportViewModel GeneratePOReport()
    {
      var reportData = new PurshaseOrderReportViewModel();

      var poId = int.Parse(reportParams.FirstOrDefault().Value);
      var companyName = db.AppSettings.FirstOrDefault(x => x.Key == "");
      var companyAddress = db.AppSettings.FirstOrDefault(x => x.Key == "");
      var companyCity = db.AppSettings.FirstOrDefault(x => x.Key == "");
      var companyState = db.AppSettings.FirstOrDefault(x => x.Key == "");
      var companyZipCode = db.AppSettings.FirstOrDefault(x => x.Key == "");

      var po = db.PurchaseOrders.FirstOrDefault(x => x.Id == poId);

      reportData.CompanyName = companyName.Value;
      reportData.CompanyAddress = companyAddress.Value;
      reportData.CompanyCity = companyCity.Value;
      reportData.CompanyState = companyState.Value;
      reportData.CompanyZipCode = companyZipCode.Value;

      reportData.OrderDate = po.Date.ToString("MM/dd/yyyy");
      reportData.PONumber = po.Ponumber;


      reportData.SupplierAddress = po.Supplier.Address;
      reportData.SupplierName = po.Supplier.Name;
      reportData.SupplierCity = po.Supplier.City;
      reportData.SupplierState = po.Supplier.State;
      reportData.SupplierZipCode = po.Supplier.ZipCode;
      reportData.SupplierEmail = po.Supplier.Email1;
      reportData.SupplierPhoneNo = po.Supplier.Phone1;

      reportData.WarehouseAddress = po.ShippedToWharehouse.Address;
      reportData.WarehouseName = po.ShippedToWharehouse.Name;
      reportData.WarehouseCity = po.ShippedToWharehouse.City;
      reportData.WarehouseState = po.ShippedToWharehouse.State;
      reportData.WarehouseZipCode = po.ShippedToWharehouse.ZipCode;
      reportData.WarehousePhoneNo = po.ShippedToWharehouse.Phone;

      reportData.SubTotal = po.SubTotal;
      reportData.Discount = po.Discount;
      reportData.TaxRate = po.TaxRate;
      reportData.TotalTax = po.TotalTaxAmount;
      reportData.ShippingCharges = po.ShipingCharge;
      reportData.TotalAmount = po.Total;

      foreach (var item in po.PurchaseOrderDetails)
      {
        var vm = new PurshaseOrderReportItemViewModel()
        {
          Description = item.Product.ProductName,
          ProductCode = item.Product.ProductCode,
          Qty = item.Qty,
          Total = item.Total,
          UnitPrice = item.UnitPrice
        };

        reportData.POItems.Add(vm);
      }



      return reportData;
    }

    private string GeneratePOReportTemplatePath()
    {
      var outPutDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
      var templatePath = Path.Combine(outPutDirectory, "ExcelTemplates\\Purchase-Order-Template.xlsx");

      return templatePath;
    }
  }
}
