using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel;
using VehicleTracker.WebApi.Infrastructure.Services;
namespace VehicleTracker.WebApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class WarehouseController : ControllerBase
  {
     private readonly IWarehouseService _warehouseService;
     private readonly IIdentityService identityService;
  
    public WarehouseController(IWarehouseService warehouseService, IIdentityService identityService)
    {
            this._warehouseService = warehouseService;
            this.identityService = identityService;
    }

    //GET api/warehouse
    [HttpGet]
    public ActionResult Get()
    {
       var response = _warehouseService.GetAllWarehouses();
       return Ok(response);
    }

    //GET api/warehouse
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
       var response = _warehouseService.GetWarehouseById(id);
       return Ok(response);
    }

    //POST api/wharehouse
    [HttpPost]
    public async Task<ActionResult>Post([FromBody] WarehouseViewModel vm)
    {
       var userName = identityService.GetUserName();
       var response = await _warehouseService.SaveWarehouse(vm, userName);
       return Ok(response);
    }

    //Delete api/whaerehouse
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
       var response = await _warehouseService.DeleteWarehouse(id);
       return Ok(response);
    }
  }
}
