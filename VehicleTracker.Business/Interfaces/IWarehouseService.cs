using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Interfaces
{
  public interface IWarehouseService
  {
    Task<ResponseViewModel> SaveWarehouse(WarehouseViewModel vm, string userName);
    Task<ResponseViewModel> DeleteWarehouse(int id);
    WarehouseViewModel GetWarehouseById(int id);
    List<WarehouseViewModel> GetAllWarehouses();
    List<DropDownViewModel> GetAllManagers(); //manegr ware house m drop down drop html 
  }
}
