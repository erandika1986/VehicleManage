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
      Statuses = new List<DropDownViewModal>();
      Customers = new List<DropDownViewModal>();
      SalesPerson = new List<DropDownViewModal>();
    }

    public List<DropDownViewModal> Statuses { get; set; }
    public List<DropDownViewModal> Customers { get; set; }
    public List<DropDownViewModal> SalesPerson { get; set; }
  }
}
