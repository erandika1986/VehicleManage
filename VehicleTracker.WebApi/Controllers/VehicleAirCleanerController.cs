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
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class VehicleAirCleanerController : ControllerBase
  {
    private readonly IVehicleAirCleanerService _vehicleAirCleanerService;
    private readonly IIdentityService identityService;
    public VehicleAirCleanerController(IVehicleAirCleanerService vehicleAirCleanerService, IIdentityService identityService)
    {
      this._vehicleAirCleanerService = vehicleAirCleanerService;
      this.identityService = identityService;
    }

    // GET api/VehicleAirCleaner/15/2
    [HttpGet]
    [Route("getAllVehicleAirCleaner/{vehicleId}")]
    public ActionResult GetAllVehicleAirCleaner(int vehicleId)
    {
      var response = _vehicleAirCleanerService.GetAllVehicleAirCleaner(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleAirCleaner/5
    [HttpGet]
    [Route("getVehicleAirCleanerById/{id}")]
    public ActionResult GetVehicleAirCleanerById(long id)
    {
      var response = _vehicleAirCleanerService.GetVehicleAirCleanerById(id);
      return Ok(response);
    }

    // POST api/VehicleAirCleaner
    [HttpPost]
    [Route("saveVehicleAirCleaner")]
    public async Task<ActionResult> SaveVehicleAirCleaner([FromBody] VehicleAirCleanerViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleAirCleanerService.SaveVehicleAirCleaner(vm, userName);
      return Ok(response);
    }



    // DELETE api/VehicleAirCleaner/5
    [HttpDelete]
    [Route("deleteVehicleAirCleaner/{id}")]
    public async Task<ActionResult> DeleteVehicleAirCleaner(int id)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleAirCleanerService.DeleteVehicleAirCleaner(id, userName);
      return Ok(response);
    }

    [HttpGet]
    [Route("getLatestRecordForVehicle/{vehicleId}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleAirCleanerService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }
  }
}
