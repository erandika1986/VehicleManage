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

  public class VehicleGreeceNipleController : ControllerBase
  {
    private readonly IVehicleGreeceNipleService _vehicleGreeceNipleService;
    private readonly IIdentityService identityService;
    public VehicleGreeceNipleController(IVehicleGreeceNipleService vehicleGreeceNipleService, IIdentityService identityService)
    {
      this._vehicleGreeceNipleService = vehicleGreeceNipleService;
      this.identityService = identityService;
    }

    // GET api/VehicleGreeceNiple/15/2
    [HttpGet]
    [Route("getAllVehicleGreeceNiple/{vehicleId}")]
    public ActionResult GetAllVehicleGreeceNiple(int vehicleId)
    {
      var response = _vehicleGreeceNipleService.GetAllVehicleGreeceNiple(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleGreeceNiple/5
    [HttpGet]
    [Route("getVehicleGreeceNipleById/{id}")]
    public ActionResult GetVehicleGreeceNipleById(long id)
    {
      var response = _vehicleGreeceNipleService.GetVehicleGreeceNipleById(id);
      return Ok(response);
    }

    // POST api/VehicleGreeceNiple
    [HttpPost]
    [Route("saveVehicleGreeceNiple")]
    public async Task<ActionResult> SaveVehicleGreeceNiple([FromBody] VehicleGreeceNipleViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleGreeceNipleService.SaveVehicleGreeceNiple(vm, userName);
      return Ok(response);
    }



    // DELETE api/VehicleGreeceNiple/5
    [HttpDelete]
    [Route("deleteVehicleGreeceNiple/{id}")]
    public async Task<ActionResult> DeleteVehicleGreeceNiple(int id)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleGreeceNipleService.DeleteVehicleGreeceNiple(id, userName);
      return Ok(response);
    }


    [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleGreeceNipleService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }
  }
}
