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
  public class VehicleRevenueLicenceController : ControllerBase
  {
    private readonly IVehicleRevenueLicenceService _vehicleRevenueLicenceService;
    private readonly IIdentityService identityService;
    public VehicleRevenueLicenceController(IVehicleRevenueLicenceService vehicleRevenueLicenceService, IIdentityService identityService)
    {
      this._vehicleRevenueLicenceService = vehicleRevenueLicenceService;
      this.identityService = identityService;
    }

    // GET api/VehicleRevenueLicence/15/2
    [HttpGet]
    [Route("getAllVehicleRevenueLicence/{vehicleId}")]
    public ActionResult GetAllVehicleRevenueLicence(int vehicleId)
    {
      var response = _vehicleRevenueLicenceService.GetAllVehicleRevenueLicence(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleRevenueLicence/5
    [HttpGet]
    [Route("getVehicleRevenueLicenceById/{id}")]
    public ActionResult Get(long id)
    {
      var response = _vehicleRevenueLicenceService.GetVehicleRevenueLicenceById(id);
      return Ok(response);
    }

    // POST api/VehicleRevenueLicence
    [HttpPost]
    [Route("saveVehicleRevenueLicence")]
    public async Task<ActionResult> SaveVehicleRevenueLicence([FromBody] VehicleRevenueLicenceViewModel vm)
    {
      var userName = IdentityHelper.GetUsername();
      var response = await _vehicleRevenueLicenceService.SaveVehicleRevenueLicence(vm, userName);
      return Ok(response);
    }


    // DELETE api/VehicleRevenueLicence/5
    [HttpDelete]
    [Route("deleteVehicleRevenueLicence/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var userName = IdentityHelper.GetUsername();
      var response = await _vehicleRevenueLicenceService.DeleteVehicleRevenueLicence(id, userName);
      return Ok(response);
    }

    [HttpGet]
    [Route("GetLatestRecordForVehicle/{vehicleId}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleRevenueLicenceService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }

    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadRevenueLicenceImage")]
    public async Task<IActionResult> UploadRevenueLicenceImage()
    {
      var userName = identityService.GetUserName();

      var container = new FileContainerModel();

      var request = await Request.ReadFormAsync();

      container.Id = int.Parse(request["id"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var response = await _vehicleRevenueLicenceService.UploadRevenueLicenceImage(container, userName);

      return Ok(response);
    }


    [HttpGet]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadRevenueLicenceImage/{id:int}")]
    [ProducesResponseType(typeof(DownloadFileViewModel), (int)HttpStatusCode.OK)]
    public FileStreamResult DownloadRevenueLicenceImage(int id)
    {
      var response = _vehicleRevenueLicenceService.DownloadInsuranceImage(id);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }
}
