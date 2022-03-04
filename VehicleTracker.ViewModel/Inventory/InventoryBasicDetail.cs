using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.Inventory
{
  public class InventoryBasicDetail
  {
    public string ProductImage { get; set; }
    public int ProductId { get; set; }
    public string CategoryName { get; set; }
    public string SubCategoryName { get; set; }
    public string ProductName { get; set; }
    public string SupplierName { get; set; }
    public int QtyInHand { get; set; }
    public int TotalItemRecieved { get; set; }
    public int TotalItemReturn { get; set; }
  }
}
