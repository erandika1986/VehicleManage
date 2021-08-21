using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.SalesOrder
{
  public class SalesOrderFilter
  {
    public int SelectedStatus { get; set; }
    public int SelectedCustomerId { get; set; }
    public int SelectedSalesPersonId { get; set; }
  }
}
