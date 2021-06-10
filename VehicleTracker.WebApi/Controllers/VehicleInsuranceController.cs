using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;
using VehicleTracker.WebApi.Helpers;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class VehicleInsuranceController : ControllerBase
  {
    private readonly IVehicleInsuranceService _vehicleInsuranceService;
    private readonly IIdentityService identityService;

    public VehicleInsuranceController(IVehicleInsuranceService vehicleInsuranceService, IIdentityService identityService)
    {
      this._vehicleInsuranceService = vehicleInsuranceService;
      this.identityService = identityService;
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
    [Route("saveVehicleInsurance")]
    public async Task<ActionResult> AddNewVehicleInsurance([FromBody] VehicleInsuranceViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleInsuranceService.SaveVehicleInsurance(vm, userName);
      return Ok(response);
    }


    // DELETE api/VehicleInsurance/5
    [HttpDelete]
    [Route("deleteVehicleInsurance/{id}")]
    public async Task<ActionResult> DeleteVehicleInsurance(int id)
    {
      var userName = identityService.GetUserName();
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

    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadInsuranceImage")]
    public async Task<IActionResult> UploadInsuranceImage()
    {
      var userName = identityService.GetUserName();

      var container = new FileContainerModel();

      var request = await Request.ReadFormAsync();

      container.Id = int.Parse(request["id"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var response = await _vehicleInsuranceService.UploadInsuranceImage(container, userName);

      return Ok(response);
    }


    [HttpGet]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadInsuranceImage/{id:int}")]
    [ProducesResponseType(typeof(DownloadFileViewModel), (int)HttpStatusCode.OK)]
    public FileStreamResult DownloadInsuranceImage(int id)
    {
      var response = _vehicleInsuranceService.DownloadInsuranceImage(id);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }
}
