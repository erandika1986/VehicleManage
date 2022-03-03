using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.SalesOrder
{
  public class SalesOrderMasterDataViewModel
  {
    public SalesOrderMasterDataViewModel()
    {
      Statuses = new List<DropDownViewModel>();
      Routes = new List<DropDownViewModel>();
      Customers = new List<DropDownViewModel>();
      SalesPerson = new List<DropDownViewModel>();
      ProductCategories = new List<DropDownViewModel>();
    }

    public List<DropDownViewModel> Statuses { get; set; }
    public List<DropDownViewModel> Routes { get; set; }
    public List<DropDownViewModel> Customers { get; set; }
    public List<DropDownViewModel> SalesPerson { get; set; }
    public List<DropDownViewModel> ProductCategories { get; set; }
  }
}
