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
      Suppliers = new List<DropDownViewModel>();
      Warehouses = new List<DropDownViewModel>();
      ProductCategories = new List<DropDownViewModel>();
      Statuses= new List<DropDownViewModel>();
    }

    public List<DropDownViewModel> Suppliers { get; set; }
    public List<DropDownViewModel> Warehouses { get; set; }
    public List<DropDownViewModel> Statuses { get; set; }
    public List<DropDownViewModel> ProductCategories { get; set; }
  }
}
