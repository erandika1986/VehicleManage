using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model.Enums;

namespace VehicleTracker.ViewModel.PurchaseOrder
{
  public class PurchaseOrderViewModel
  {
    public PurchaseOrderViewModel()
    {
      Items = new List<PurchaseOrderItemViewModel>();
    }
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string PONumber { get; set; }
    public int SelectedSupplierId { get; set; }
    public int SelectedWarehouseId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TotalTaxAmout { get; set; }
    public decimal ShippingCharge { get; set; }
    public decimal Total { get; set; }
    public POStatus Status { get; set; }
    public string Remarks { get; set; }

    public string CreatedBy { get; set; }
    public string CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public string UpdatedOn { get; set; }

    public List<PurchaseOrderItemViewModel> Items { get; set; }
  }
}
