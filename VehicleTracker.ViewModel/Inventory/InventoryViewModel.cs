using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.Inventory
{

  public class POInventoryReceievedDetail
  {
    public POInventoryReceievedDetail()
    {
      Inventories = new List<InventoryViewModel>();
    }

    public int Id { get; set; }
    public int WarehouseId { get; set; }
    public int PuchaseOrderId { get; set; }
    public List<InventoryViewModel> Inventories { get; set; }
  }
  public class InventoryViewModel
  {
    public int Id { get; set; }
    public DateTime DateRecieved { get; set; }
    public string BatchNo { get; set; }
    public DateTime? DateOfManufacture { get; set; }
    public DateTime? DateOfExpiration { get; set; }
    public string ProductCategoryName { get; set; }
    public string ProductSubCategoryName { get; set; }
    public string SupplierName { get; set; }
    public string ProductName { get; set; }
    public int ProductId { get; set; }
    public int TotalOrderedQty { get; set; }
    public int RecievedQty { get; set; }
    public bool IsActive { get; set; }
  }


}
