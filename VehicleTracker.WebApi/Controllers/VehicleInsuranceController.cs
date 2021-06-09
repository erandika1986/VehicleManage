using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.Vehicle;
using VehicleTracker.WebApi.Helpers;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class VehicleInsuranceController : ControllerBase
  {
    private readonly IVehicleInsuranceService _vehicleInsuranceService;

    public VehicleInsuranceController(IVehicleInsuranceService vehicleInsuranceService)
    {
      this._vehicleInsuranceService = vehicleInsuranceService;
    }

    // GET api/VehicleInsurance/15/2
    [HttpGet]
    [Route("getAllVehicleInsurance/{vehicleId}")]
    public ActionResult GetAllVehicleInsurance(int vehicleId)
    {
      var response = _vehicleInsuranceService.GetAllVehicleInsurance(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleInsurance/5
    [HttpGet]
    [Route("getVehicleInsuranceById/{id}")]
    public ActionResult GetVehicleInsuranceById(long id)
    {
      var response = _vehicleInsuranceService.GetVehicleInsuranceById(id);
      return Ok(response);
    }

    // POST api/VehicleInsurance
    [HttpPost]
    [Route("addNewVehicleInsurance")]
    public async Task<ActionResult> AddNewVehicleInsurance([FromBody] VehicleInsuranceViewModel vm)
    {
      var userName = IdentityHelper.GetUsername();
      var response = await _vehicleInsuranceService.SaveVehicleInsurance(vm, userName);
      return Ok(response);
    }


    // DELETE api/VehicleInsurance/5
    [HttpDelete]
    [Route("deleteVehicleInsurance/{id}")]
    public async Task<ActionResult> DeleteVehicleInsurance(int id)
    {
      var userName = IdentityHelper.GetUsername();
      var response = await _vehicleInsuranceService.DeleteVehicleInsurance(id, userName);
      return Ok(response);
    }

    [HttpGet]
    [Route("getLatestRecordForVehicle/{vehicleId}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleInsuranceService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }
  }
}
