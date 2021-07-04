using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.PurchaseOrder
{
  public class PurchaseOrderMasterData
  {
    public PurchaseOrderMasterData()
    {
      Suppliers = new List<DropDownViewModal>();
      Warehouses = new List<DropDownViewModal>();
      ProductCategories = new List<DropDownViewModal>();
      Statuses= new List<DropDownViewModal>();
    }

    public List<DropDownViewModal> Suppliers { get; set; }
    public List<DropDownViewModal> Warehouses { get; set; }
    public List<DropDownViewModal> Statuses { get; set; }
    public List<DropDownViewModal> ProductCategories { get; set; }
  }
}
