using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.PurchaseOrder
{
  public class PurchaseOrderFilter
  {
    public int SelectedStatusId { get; set; }
    public int SelectedSupplierId { get; set; }
    public int SelectedWarehouseNameId { get; set; }
  }
}
