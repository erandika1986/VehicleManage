using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.Vehicle;
using VehicleTracker.WebApi.Helpers;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class VehicleDifferentialOilChangeMilageController : ControllerBase
  {
    private readonly IVehicleDifferentialOilChangeMilageService _vehicleDifferentialOilChangeMilageService;
    private readonly IIdentityService identityService;

    public VehicleDifferentialOilChangeMilageController(IVehicleDifferentialOilChangeMilageService vehicleDifferentialOilChangeMilageService, IIdentityService identityService)
    {
      this._vehicleDifferentialOilChangeMilageService = vehicleDifferentialOilChangeMilageService;
      this.identityService = identityService;
    }

    // GET api/VehicleDifferentialOilChangeMilage/15/2
    [HttpGet("{vehicleId:int}")]
    public ActionResult Get(int vehicleId)
    {
      var response = _vehicleDifferentialOilChangeMilageService.GetAllVehicleDifferentialOilChangeMilage(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleDifferentialOilChangeMilage/5
    [HttpGet("{id}")]
    public ActionResult Get(long id)
    {
      var response = _vehicleDifferentialOilChangeMilageService.GetVehicleDifferentialOilChangeMilageById(id);
      return Ok(response);
    }

    // POST api/VehicleDifferentialOilChangeMilage
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VehicleDifferentialOilChangeMilageViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleDifferentialOilChangeMilageService.SaveVehicleDifferentialOilChangeMilage(vm, userName);
      return Ok(response);
    }



    // DELETE api/VehicleDifferentialOilChangeMilage/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleDifferentialOilChangeMilageService.DeleteVehicleDifferentialOilChangeMilage(id, userName);
      return Ok(response);
    }

    [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleDifferentialOilChangeMilageService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }
  }
}
