using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.SalesOrder
{
  public class BasicSalesOrderDetailViewModel
  {
    public long Id { get; set; }
    public string OrderNumber { get; set; }
    public decimal Total { get; set; }
    public int TotalQty { get; set; }
    public string Status { get; set; }
    public string OrderDate { get; set; }
    public string OwnderName { get; set; }
    public string OwnerAddress { get; set; }
    public string Route { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public string UpdatedOn { get; set; }
  }
}
