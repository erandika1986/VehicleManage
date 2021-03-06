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
  public class VehicleEngineOilMilageController : ControllerBase
  {
    private readonly IVehicleEngineOilMilageService _vehicleEngineOilMilageService;
    private readonly IIdentityService identityService;

    public VehicleEngineOilMilageController(IVehicleEngineOilMilageService vehicleEngineOilMilageService, IIdentityService identityService)
    {
      this._vehicleEngineOilMilageService = vehicleEngineOilMilageService;
      this.identityService = identityService;
    }

    // GET api/VehicleEngineOilMilage/15/2
    [HttpGet]
    [Route("getAllVehicleEngineOilMilage/{vehicleId}")]
    public ActionResult GetAllVehicleEngineOilMilage(int vehicleId)
    {
      var response = _vehicleEngineOilMilageService.GetAllVehicleEngineOilMilage(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleEngineOilMilage/5
    [HttpGet]
    [Route("getVehicleEngineOilMilageById/{id}")]
    public ActionResult Get(long id)
    {
      var response = _vehicleEngineOilMilageService.GetVehicleEngineOilMilageById(id);
      return Ok(response);
    }

    // POST api/VehicleEngineOilMilage
    [HttpPost]
    [Route("saveVehicleEngineOilMilage")]
    public async Task<ActionResult> SaveVehicleEngineOilMilage([FromBody] VehicleEngineOilMilageViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleEngineOilMilageService.SaveVehicleEngineOilMilage(vm, userName);
      return Ok(response);
    }



    // DELETE api/VehicleEngineOilMilage/5
    [HttpDelete]
    [Route("deleteVehicleEngineOilMilage/{id}")]
    public async Task<ActionResult> DeleteVehicleEngineOilMilage(int id)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleEngineOilMilageService.DeleteVehicleEngineOilMilage(id, userName);
      return Ok(response);
    }

    [HttpGet]
    [Route("getLatestRecordForVehicle/{vehicleId}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleEngineOilMilageService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }
  }
}
