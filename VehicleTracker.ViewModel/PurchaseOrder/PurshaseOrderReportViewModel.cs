using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.PurchaseOrder
{
  public class PurshaseOrderReportViewModel
  {
    public PurshaseOrderReportViewModel()
    {
      POItems = new List<PurshaseOrderReportItemViewModel>();
    }

    public string CompanyName { get; set; }
    public string CompanyAddress { get; set; }
    public string CompanyCity { get; set; }
    public string CompanyState { get; set; }
    public string CompanyZipCode { get; set; }
    public string CompanyEmail { get; set; }
    public string CompanyPhoneNo { get; set; }
    public string CompanyCountry { get; set; }

    public string OrderDate { get; set; }
    public string PONumber { get; set; }

     
    public string SupplierName { get; set; }
    public string SupplierAddress { get; set; }
    public string SupplierCity { get; set; }
    public string SupplierState { get; set; }
    public string SupplierZipCode { get; set; }
    public string SupplierEmail { get; set; }
    public string SupplierPhoneNo { get; set; }
    public string SupplierCountry { get; set; }
    public string CustomerRef { get; set; }


    public string WarehouseName { get; set; }
    public string WarehouseAddress { get; set; }
    public string WarehouseCity { get; set; }
    public string WarehouseState { get; set; }
    public string WarehouseZipCode { get; set; }
    public string WarehouseCountry { get; set; }
    public string WarehouseEmail { get; set; }
    public string WarehousePhoneNo { get; set; }

    public string ShippingMethod { get; set; }
    public string ShippingTerm { get; set; }
    public string ShipVia { get; set; }
    public string Payment { get; set; }
    public string DeliveryDate { get; set; }

    public string Remarks { get; set; }

    public List<PurshaseOrderReportItemViewModel> POItems { get; set; }



    public decimal SubTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TotalTax { get; set; }
    public decimal ShippingCharges { get; set; }
    public decimal TotalAmount { get; set; }
  }


  public class PurshaseOrderReportItemViewModel
  {
    public string ProductCode { get; set; }
    public string Description { get; set; }
    public int Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
  }
}
