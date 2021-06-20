using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business
{
  public class WarehouseService : IWarehouseService
  {
    #region Member variable

    private readonly VMDBContext _db;
    private readonly ILogger<IWarehouseService> _logger;
    private readonly IConfiguration _config;


    #endregion


    public WarehouseService(VMDBContext db, ILogger<IWarehouseService> logger, IConfiguration config)
    {
      this._db = db;
      this._logger = logger;
      this._config = config;
    }


    public async Task<ResponseViewModel> DeleteWarehouse(int id)
    {
            var response = new ResponseViewModel();

            try
            {
                var warehouse = _db.Wharehouses.FirstOrDefault(x => x.Id == id);

                warehouse.IsActive = false;

                _db.Wharehouses.Update(warehouse);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Warehouse has been deleted.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured.Please try again.";
            }

            return response;
    }

    public List<WarehouseViewModel> GetAllWarehouses()
    {
            var response = new List<WarehouseViewModel>();

            var query = _db.Wharehouses.Where(t => t.IsActive == true);

            var results = query.ToList();

            foreach(var warehouse in results)
            {
                var vm = new WarehouseViewModel();
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

                response.Add(vm);
            }



            
      return response;
    }

    public WarehouseViewModel GetWarehouseById(int id)
    {
      var response = new WarehouseViewModel();

      var warehouse = _db.Wharehouses.FirstOrDefault(x => x.Id == id);

      response.Id = warehouse.Id;
      response.Address = warehouse.Address;
      response.CreatedBy = string.Format("{0} {1}", warehouse.CreatedBy.FirstName, warehouse.CreatedBy.LastName);
      response.CreatedOn = warehouse.CreatedOn.ToString("MMMM dd, yyyy");
      response.FloorSpace = warehouse.FloorSpace;
      response.IsActive = warehouse.IsActive;
      response.ManagerName = string.Format("{0} {1}", warehouse.Manager.FirstName, warehouse.Manager.LastName);
      response.Phone = warehouse.Phone;
      response.UpdatedOn = warehouse.UpdatedOn.ToString("MMMM dd, yyyy");
      response.UpdatedBy = string.Format("{0} {1}", warehouse.UpdatedBy.FirstName, warehouse.UpdatedBy.LastName);

      return response;
    }

    public async Task<ResponseViewModel> SaveWarehouse(WarehouseViewModel vm)
    {
      var response = new ResponseViewModel();

      try
      {
        var warehouse = _db.Wharehouses.FirstOrDefault(x => x.Id == vm.Id);

        if(warehouse==null)
        {
                    
                    //Create new Warehouse object and save it to the database
                    _db.Wharehouses.Add(warehouse);
                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Warehouse has been created.";

                }
        else
        {
                    //Updated existing record with given value;
                    warehouse.Id = vm.Id;
                    warehouse.Address = vm.Address;
                    warehouse.Phone = vm.Phone;
                    warehouse.ManagerId = vm.SelectedManagerId;
                    warehouse.FloorSpace = vm.FloorSpace;

                    _db.Wharehouses.Update(warehouse);
                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Warehouse has been updated.";
         }

       /* response.IsSuccess = true;
        response.Message = "";*/
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured. Please try again.";
      }

      return response;
    }
  }
}
