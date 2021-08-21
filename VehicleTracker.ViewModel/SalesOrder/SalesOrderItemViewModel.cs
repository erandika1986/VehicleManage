using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.SalesOrder
{
  public class SalesOrderItemViewModel
  {
    public int Id { get; set; }
    public int SalesOrderId { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
  }
}
