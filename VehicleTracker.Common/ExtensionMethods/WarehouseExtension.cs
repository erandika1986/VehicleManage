using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;
using VehicleTracker.ViewModel;

namespace System
{
  public static class WarehouseExtension
  {
    public static WarehouseViewModel ToVM(this Wharehouse warehouse, WarehouseViewModel vm=null)
    {
      if(vm==null)
        vm = new WarehouseViewModel();

      vm.Id = warehouse.Id;
      vm.Address = warehouse.Address;
      vm.CreatedBy = string.Format("{0} {1}", warehouse.CreatedBy.FirstName, warehouse.CreatedBy.LastName);
      vm.CreatedOn = warehouse.CreatedOn.ToString("MMMM dd, yyyy");
      vm.FloorSpace = warehouse.FloorSpace;
      vm.IsActive = warehouse.IsActive;
      vm.ManagerName = string.Format("{0} {1}", warehouse.Manager.FirstName, warehouse.Manager.LastName);
      vm.Phone = warehouse.Phone;
      vm.UpdatedOn = warehouse.UpdatedOn.ToString("MMMM dd, yyyy");
      vm.UpdatedBy = string.Format("{0} {1}", warehouse.UpdatedBy.FirstName, warehouse.UpdatedBy.LastName);

      return vm;
    }

    public static Wharehouse ToModel(this WarehouseViewModel vm, Wharehouse model = null)
    {
      if (model == null)
        model = new Wharehouse();


      model.Address = vm.Address;
      model.CreatedOn = DateTime.UtcNow;
      model.FloorSpace = vm.FloorSpace;
      model.ManagerId = vm.SelectedManagerId;
      model.Phone = vm.Phone;
      model.UpdatedOn = DateTime.UtcNow;

      return model;
    }
  }
}
