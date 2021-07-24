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

    public DateTime OrderDate { get; set; }
    public string PONumber { get; set; }

    public string SupplierName { get; set; }
    public string VenderStreeetAddress { get; set; }
    public string VenderCity { get; set; }
    public string VendorZipCode { get; set; }

    public bool IsOtherShipToAddress { get; set; }
    public string CompanyShipToAddress { get; set; }
    public string CompanyCity { get; set; }
    public string CompanyZipCode { get; set; }
    public List<PurshaseOrderReportItemViewModel> POItems { get; set; }

    public decimal TotalAmount { get; set; }
  }

  public class PurshaseOrderReportItemViewModel
  {
    public string Description { get; set; }
    public int Qty { get; set; }
    public int UM { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
  }
}
