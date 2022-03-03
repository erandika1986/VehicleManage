using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.Model.Enums;
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
    private readonly IUserService _userService;


    #endregion


    #region Constructors

    public WarehouseService(VMDBContext db, ILogger<IWarehouseService> logger, IConfiguration config, IUserService userService)
    {
      this._db = db;
      this._logger = logger;
      this._config = config;
      this._userService = userService;
    }

    #endregion

    #region Public Methods

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
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured.Please try again.";
      }

      return response;
    }

    public List<DropDownViewModel> GetAllManagers()
    {
            var managers = _db.UserRoles
                .Where(x => x.RoleId == (int)RoleType.Manager || x.RoleId == (int)RoleType.WarehouseManager)
                .Select(u => new DropDownViewModel() { Id = u.User.Id, Name = string.Format("{0} {1}", u.User.FirstName, u.User.LastName) }).Distinct().ToList();

            return managers;
        }

    public List<WarehouseViewModel> GetAllWarehouses()
    {
      var response = new List<WarehouseViewModel>();

      var query = _db.Wharehouses.Where(t => t.IsActive == true);

      var results = query.ToList();

      foreach (var warehouse in results)
      {
      
        response.Add(warehouse.ToVM());
      }

      return response;
    }

    public WarehouseViewModel GetWarehouseById(int id)
    {

      var warehouse = _db.Wharehouses.FirstOrDefault(x => x.Id == id);

      var response = warehouse.ToVM(); ;    

      return response;
    }

    public async Task<ResponseViewModel> SaveWarehouse(WarehouseViewModel vm, string userName)
    {
      var response = new ResponseViewModel();

      try
      {
        var warehouse = _db.Wharehouses.FirstOrDefault(x => x.Id == vm.Id);
        var user = _userService.GetUserByUsername(userName);
        if (warehouse == null)
        {
          warehouse = vm.ToModel();
          warehouse.CreatedById = user.Id;
          warehouse.UpdatedById = user.Id;
          //Create new Warehouse object and save it to the database
          _db.Wharehouses.Add(warehouse);
          await _db.SaveChangesAsync();

          response.IsSuccess = true;
          response.Message = "Warehouse has been created.";

        }
        else
        {
          //Updated existing record with given value;
          warehouse.Name = vm.Name;
          warehouse.Address = vm.Address;
          warehouse.State = vm.State;
          warehouse.City = vm.City;
          warehouse.ZipCode = vm.ZipCode;
          warehouse.Phone = vm.Phone;
          warehouse.ManagerId = vm.SelectedManagerId;
          warehouse.FloorSpace = vm.FloorSpace;
          warehouse.UpdatedById = user.Id;
          warehouse.UpdatedOn = DateTime.UtcNow;

          _db.Wharehouses.Update(warehouse);
          await _db.SaveChangesAsync();

          response.IsSuccess = true;
          response.Message = "Warehouse has been updated.";
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured. Please try again.";
      }

      return response;
    }

  

        #endregion



    }

}
