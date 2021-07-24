using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.PurchaseOrder
{
  public class PurchaseOrderItemViewModel
  {

    public PurchaseOrderItemViewModel()
    {
      Categories = new List<DropDownViewModal>();
      SubCategories = new List<DropDownViewModal>();
      Products = new List<DropDownViewModal>();
    }

    public int Id { get; set; }
    public int PurchaseOrderId { get; set; }
    public int SelectedCategoryId { get; set; }
    public int SelectedSubCategoryId { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }


    public List<DropDownViewModal> Categories { get; set; }
    public List<DropDownViewModal> SubCategories { get; set; }
    public List<DropDownViewModal> Products { get; set; }
  }
}
