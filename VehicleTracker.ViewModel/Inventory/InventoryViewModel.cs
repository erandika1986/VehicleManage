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

    public int POId { get; set; }
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
    public int ProductId { get; set; }
    public int RecievedQty { get; set; }
    public int AvailableQty { get; set; }
    public bool IsActive { get; set; }
  }


}
