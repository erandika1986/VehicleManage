using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.PurchaseOrder
{
  public class PurchaseOrderSummaryViewModel
  {
    public int Id { get; set; }
    public string PONumber { get; set; }
    public string SupplierName { get; set; }
    public string WarehouseName { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal ShippingCharges { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
    public string Date { get; set; }
  }
}
