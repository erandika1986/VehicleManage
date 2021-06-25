using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel
{
  public class WarehouseViewModel
  {
    public int Id { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string ManagerName { get; set; }
    public decimal? FloorSpace { get; set; }
    public string CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsActive { get; set; }


    public long SelectedManagerId { get; set; }
  }
}
