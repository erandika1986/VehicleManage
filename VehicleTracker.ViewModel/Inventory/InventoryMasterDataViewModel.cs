using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.Inventory
{
  public class InventoryMasterDataViewModel
  {
    public InventoryMasterDataViewModel()
    {
      Suppliers = new List<DropDownViewModel>();
      Warehouses = new List<DropDownViewModel>();
      ProductCategories = new List<DropDownViewModel>();
      ActivePurchaseOrders = new List<DropDownViewModel>();
    }

    public List<DropDownViewModel> ActivePurchaseOrders { get; set; }
    public List<DropDownViewModel> Suppliers { get; set; }
    public List<DropDownViewModel> Warehouses { get; set; }
    public List<DropDownViewModel> ProductCategories { get; set; }

  }
}
