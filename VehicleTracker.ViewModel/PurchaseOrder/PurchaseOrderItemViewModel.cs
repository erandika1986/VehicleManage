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
      Categories = new List<DropDownViewModel>();
      SubCategories = new List<DropDownViewModel>();
      Products = new List<DropDownViewModel>();
    }

    public int Id { get; set; }
    public int PurchaseOrderId { get; set; }
    public int SelectedCategoryId { get; set; }
    public int SelectedSubCategoryId { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }


    public List<DropDownViewModel> Categories { get; set; }
    public List<DropDownViewModel> SubCategories { get; set; }
    public List<DropDownViewModel> Products { get; set; }
  }
}
