using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
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
using Spire.Xls;

namespace VehicleTracker.Report
{
  public class POReportGenerator: BaseReportGenerator
  {
    public POReportGenerator(Dictionary<string, string> reportParams, VMDBContext db, IConfiguration config)
      : base(reportParams, db, config)
    {
      
    }

    public override DownloadFileViewModel GenerateExcelReport()
    {
      return base.GenerateExcelReport();
    }

    public override DownloadFileViewModel GeneratePDFReport()
    {
      var file = new DownloadFileViewModel();

      var reportData = GeneratePOReport();
      var fileIdentifier = DateTime.Now.ToString("yyyyMMddHHmmssfff");
      var excelName = string.Format("{0}{1}.xlsx", reportData.PONumber, fileIdentifier);
      var pdfName = string.Format("{0}{1}.pdf", reportData.PONumber, fileIdentifier);

      var excelSection = config.GetSection("Report");

      var pdfSavingPath = excelSection.GetSection("PDFReportSavingPath").Value;

      var excelReportPath = string.Format("{0}{1}", pdfSavingPath, excelName);
      var pdfReportPath = string.Format("{0}{1}", pdfSavingPath, pdfName);
      var templatePath = GeneratePOReportTemplatePath();

      File.Copy(templatePath, excelReportPath);

      FileInfo newFile = new FileInfo(excelReportPath);

      int subTotalStartAt = 38;

      using (var package = new ExcelPackage(newFile))
      {
        var workSheet = package.Workbook.Worksheets["Purchase-Order"];

        //For Company Details
        workSheet.Cells[2, 2].Value = reportData.CompanyName;
        workSheet.Cells[3, 2].Value = reportData.CompanyAddress;
        workSheet.Cells[4, 2].Value = reportData.CompanyCity;
        workSheet.Cells[5, 2].Value = $"{reportData.CompanyState},{reportData.CompanyZipCode}.{reportData.CompanyCountry}";
        workSheet.Cells[6, 2].Value = reportData.CompanyPhoneNo;
        workSheet.Cells[7, 2].Value = reportData.CompanyEmail;

        //For Supplier Details
        workSheet.Cells[10, 2].Value = "ATTN: Sales Department";
        workSheet.Cells[11, 2].Value = reportData.SupplierName;
        workSheet.Cells[12, 2].Value = reportData.SupplierAddress;
        workSheet.Cells[13, 2].Value = reportData.SupplierCity;
        workSheet.Cells[14, 2].Value = $"{reportData.SupplierState}, {reportData.SupplierZipCode} {reportData.SupplierCountry}";
        workSheet.Cells[15, 2].Value = reportData.SupplierPhoneNo;
        workSheet.Cells[16, 2].Value = reportData.SupplierEmail;

        //For Shipping Details
        workSheet.Cells[10, 5].Value = "ATTN: Purchasing Department";
        workSheet.Cells[11, 5].Value = reportData.WarehouseName;
        workSheet.Cells[12, 5].Value = reportData.WarehouseAddress;
        workSheet.Cells[13, 5].Value = reportData.WarehouseCity;
        workSheet.Cells[14, 5].Value = $"{reportData.WarehouseState}, {reportData.WarehouseZipCode} {reportData.CompanyCountry}";
        workSheet.Cells[15, 5].Value = reportData.WarehousePhoneNo;
        workSheet.Cells[16, 5].Value = reportData.WarehouseEmail;

        //For Order Details
        workSheet.Cells[2, 5].Value = $"DATE : {reportData.OrderDate}";
        workSheet.Cells[4, 5].Value = $"PURCHASE ORDER NO : {reportData.PONumber}";
        workSheet.Cells[6, 5].Value = $"CUSTOMER NO : {reportData.CustomerRef}";


        workSheet.Cells[20, 2].Value = reportData.ShippingMethod;
        workSheet.Cells[20, 3].Value = reportData.ShippingTerm;
        workSheet.Cells[20, 4].Value = reportData.ShipVia;
        workSheet.Cells[20, 5].Value = reportData.Payment;
        workSheet.Cells[20, 6].Value = reportData.DeliveryDate;

        if(reportData.POItems.Count>15)
        {
          var noOfRowNeedToAdd = reportData.POItems.Count - 15;
          workSheet.InsertRow(36, noOfRowNeedToAdd);
          subTotalStartAt = subTotalStartAt + noOfRowNeedToAdd;
        }

        var startIndex = 23;
        foreach (var item in reportData.POItems)
        {
          workSheet.Cells[startIndex, 2].Value = item.ProductCode;
          workSheet.Cells[startIndex, 3].Value = item.Description;
          workSheet.Cells[startIndex, 4].Value = item.Qty;
          workSheet.Cells[startIndex, 5].Value = item.UnitPrice;
          workSheet.Cells[startIndex, 6].Value = item.Total;

          startIndex++;
        }

        workSheet.Cells[subTotalStartAt+1, 6].Value = reportData.Discount;
        workSheet.Cells[subTotalStartAt + 3, 6].Value = reportData.TaxRate/100;
        workSheet.Cells[subTotalStartAt + 5, 6].Value = reportData.ShippingCharges;
        workSheet.Cells[subTotalStartAt + 1, 2].Value = reportData.Remarks;
        package.Save();


      }


      Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
      //Load excel file  
      workbook.LoadFromFile(newFile.FullName);
      //Save excel file to pdf file.  
      workbook.SaveToFile(pdfReportPath, Spire.Xls.FileFormat.PDF);

      byte[] fileContents = null;
      MemoryStream ms = new MemoryStream();

      using (FileStream fs = File.OpenRead(pdfReportPath))
      {
        fs.CopyTo(ms);
        fileContents = ms.ToArray();
        ms.Dispose();
        file.FileData = fileContents;
      }

      file.FileName = pdfName;

      return file;
    }


    private PurshaseOrderReportViewModel GeneratePOReport()
    {
      var reportData = new PurshaseOrderReportViewModel();

      var poId = int.Parse(reportParams.FirstOrDefault().Value);
      var companyName = db.AppSettings.FirstOrDefault(x => x.Key == "CompanyName");
      var companyAddress = db.AppSettings.FirstOrDefault(x => x.Key == "CompanyAddress");
      var companyCity = db.AppSettings.FirstOrDefault(x => x.Key == "CompanyCity");
      var companyState = db.AppSettings.FirstOrDefault(x => x.Key == "CompanyState");
      var companyZipCode = db.AppSettings.FirstOrDefault(x => x.Key == "CompanyZipCode");
      var companyPhone = db.AppSettings.FirstOrDefault(x => x.Key == "CompanyPrimaryPhone");
      var companyEmail = db.AppSettings.FirstOrDefault(x => x.Key == "CompanyPrimaryEmail");
      var companyCountry = db.AppSettings.FirstOrDefault(x => x.Key == "CompanyCountry");

      var po = db.PurchaseOrders.FirstOrDefault(x => x.Id == poId);

      reportData.CompanyName = companyName.Value;
      reportData.CompanyAddress = companyAddress.Value;
      reportData.CompanyCity = companyCity.Value;
      reportData.CompanyState = companyState.Value;
      reportData.CompanyZipCode = companyZipCode.Value;
      reportData.CompanyEmail = companyEmail.Value;
      reportData.CompanyPhoneNo = companyPhone.Value;

      reportData.OrderDate = po.Date.ToString("MM/dd/yyyy");
      reportData.PONumber = po.Ponumber;


      reportData.SupplierAddress = po.Supplier.Address;
      reportData.SupplierName = po.Supplier.Name;
      reportData.SupplierCity = po.Supplier.City;
      reportData.SupplierState = po.Supplier.State;
      reportData.SupplierZipCode = po.Supplier.ZipCode;
      reportData.SupplierEmail = po.Supplier.Email1;
      reportData.SupplierPhoneNo = po.Supplier.Phone1;
      reportData.SupplierCountry = po.Supplier.Country;
      reportData.CustomerRef = po.Supplier.OurRefNo;

      reportData.WarehouseAddress = po.ShippedToWharehouse.Address;
      reportData.WarehouseName = po.ShippedToWharehouse.Name;
      reportData.WarehouseCity = po.ShippedToWharehouse.City;
      reportData.WarehouseState = po.ShippedToWharehouse.State;
      reportData.WarehouseZipCode = po.ShippedToWharehouse.ZipCode;
      reportData.WarehousePhoneNo = po.ShippedToWharehouse.Phone;
      reportData.WarehouseCountry = po.ShippedToWharehouse.Country;
      reportData.WarehouseEmail = companyEmail.Value;

      reportData.SubTotal = po.SubTotal;
      reportData.Discount = po.Discount;
      reportData.TaxRate = po.TaxRate;
      reportData.TotalTax = po.TotalTaxAmount;
      reportData.ShippingCharges = po.ShipingCharge;
      reportData.TotalAmount = po.Total;
      reportData.Remarks = po.Remark;

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
