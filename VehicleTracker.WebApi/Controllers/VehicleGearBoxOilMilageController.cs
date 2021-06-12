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
  public class VehicleGearBoxOilMilageController : ControllerBase
  {
    private readonly IVehicleGearBoxOilMilageService _vehicleGearBoxOilMilageService;
    private readonly IIdentityService identityService;
    public VehicleGearBoxOilMilageController(IVehicleGearBoxOilMilageService vehicleGearBoxOilMilageService, IIdentityService identityService)
    {
      this._vehicleGearBoxOilMilageService = vehicleGearBoxOilMilageService;
      this.identityService = identityService;
    }

    // GET api/VehicleGearBoxOilMilage/15/2
    [HttpGet("{vehicleId:int}")]
    public ActionResult Get(int vehicleId)
    {
      var response = _vehicleGearBoxOilMilageService.GetAllVehicleGearBoxOilMilage(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleGearBoxOilMilage/5
    [HttpGet("{id}")]
    public ActionResult Get(long id)
    {
      var response = _vehicleGearBoxOilMilageService.GetVehicleGearBoxOilMilageById(id);
      return Ok(response);
    }

    // POST api/VehicleGearBoxOilMilage
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VehicleGearBoxOilMilageViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleGearBoxOilMilageService.SaveVehicleGearBoxOilMilage(vm, userName);
      return Ok(response);
    }



    // DELETE api/VehicleGearBoxOilMilage/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleGearBoxOilMilageService.DeleteVehicleGearBoxOilMilage(id, userName);
      return Ok(response);
    }

    [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleGearBoxOilMilageService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }
  }
}
