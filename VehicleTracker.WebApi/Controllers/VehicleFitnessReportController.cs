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
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class VehicleFitnessReportController : ControllerBase
  {
    private readonly IVehicleFitnessReportService _vehicleFitnessReportService;
    private readonly IIdentityService identityService;

    public VehicleFitnessReportController(IVehicleFitnessReportService vehicleFitnessReportService, IIdentityService identityService)
    {
      this._vehicleFitnessReportService = vehicleFitnessReportService;
      this.identityService = identityService;
    }

    // GET api/VehicleFitnessReport/15/2
    [HttpGet]
    [Route("getAllVehicleFitnessReport/{vehicleId}")]
    public ActionResult GetAllVehicleFitnessReport(int vehicleId)
    {
      var response = _vehicleFitnessReportService.GetAllVehicleFitnessReport(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleFitnessReport/5
    [HttpGet]
    [Route("getVehicleFitnessReportById/{id}")]
    public ActionResult GetVehicleFitnessReportById(long id)
    {
      var response = _vehicleFitnessReportService.GetVehicleFitnessReportById(id);
      return Ok(response);
    }

    // POST api/VehicleFitnessReport
    [HttpPost]
    [Route("saveVehicleFitnessReport")]
    public async Task<ActionResult> Post([FromBody] VehicleFitnessReportViewModel vm)
    {
      var userName = IdentityHelper.GetUsername();
      var response = await _vehicleFitnessReportService.SaveVehicleFitnessReport(vm, userName);
      return Ok(response);
    }



    // DELETE api/VehicleFitnessReport/5
    [HttpDelete]
    [Route("deleteVehicleFitnessReport/{id}")]
    public async Task<ActionResult> DeleteVehicleFitnessReport(int id)
    {
      var userName = IdentityHelper.GetUsername();
      var response = await _vehicleFitnessReportService.DeleteVehicleFitnessReport(id, userName);
      return Ok(response);
    }

    [HttpGet]
    [Route("getLatestRecordForVehicle/{vehicleId}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleFitnessReportService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }

    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadFitnessReportImage")]
    public async Task<IActionResult> UploadFitnessReportImage()
    {
      var userName = identityService.GetUserName();

      var container = new FileContainerModel();

      var request = await Request.ReadFormAsync();

      container.Id = int.Parse(request["id"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var response = await _vehicleFitnessReportService.UploadFitnessReportImage(container, userName);

      return Ok(response);
    }


    [HttpGet]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadFitnessReportImage/{id:int}")]
    [ProducesResponseType(typeof(DownloadFileViewModel), (int)HttpStatusCode.OK)]
    public FileStreamResult DownloadFitnessReportImage(int id)
    {
      var response = _vehicleFitnessReportService.DownloadFitnessReportImage(id);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }
}
