using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.Inventory
{
  public class InventoryFilter
  {
    public int SelectedProductSubCategoryId { get; set; }
    public int SelectedProductId { get; set; }
    public int SelectedSupplierId { get; set; }
  }
}
