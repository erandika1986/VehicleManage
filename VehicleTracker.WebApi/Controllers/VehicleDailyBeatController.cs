using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.VehicleBeat;
using VehicleTracker.WebApi.Helpers;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VehicleDailyBeatController : ControllerBase
  {
    private readonly IVehicleBeatService _vehicleBeatService;
    private readonly IIdentityService identityService;

    public VehicleDailyBeatController(IVehicleBeatService vehicleBeatService, IIdentityService identityService)
    {
      this._vehicleBeatService = vehicleBeatService;
      this.identityService = identityService;
    }



    [HttpPost("getAllVehicleBeatRecord")]
    public ActionResult Post(VehicleBeatFilterViewModel filters)
    {
      var userName = identityService.GetUserName();
      var response = _vehicleBeatService.GetAllVehicleBeatRecord(filters, userName);
      return Ok(response);
    }


    [HttpPost]
    public async Task<ActionResult> Post([FromBody] DailyVehicleBeatViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleBeatService.SaveDailyVehicleBeatRecord(vm, userName);
      return Ok(response);
    }

    //[HttpPut]
    // public async Task<ActionResult> Put([FromBody] DailyVehicleBeatViewModel vm)
    //{
    //   var userName = IdentityHelper.GetUsername();
    //  var response = await _vehicleBeatService.UpdateNewVehicleBeatRecord(vm, userName);
    //   return Ok(response);
    // }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleBeatService.DeleteSelectedBeatRecord(id, userName);
      return Ok(response);
    }



    [HttpGet("{id}")]
    public ActionResult Get(long id)
    {
      var response = _vehicleBeatService.GetVehicleBeatRecordById(id);
      return Ok(response);
    }


    [HttpGet("getMasterData")]
    public ActionResult Get()
    {
      var response = _vehicleBeatService.GetMasterData();
      return Ok(response);
    }
  }
}
