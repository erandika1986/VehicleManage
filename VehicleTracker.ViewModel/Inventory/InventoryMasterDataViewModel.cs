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
      Suppliers = new List<DropDownViewModal>();
      Warehouses = new List<DropDownViewModal>();
      ProductCategories = new List<DropDownViewModal>();
    }

    public List<DropDownViewModal> Suppliers { get; set; }
    public List<DropDownViewModal> Warehouses { get; set; }
    public List<DropDownViewModal> ProductCategories { get; set; }

  }
}
