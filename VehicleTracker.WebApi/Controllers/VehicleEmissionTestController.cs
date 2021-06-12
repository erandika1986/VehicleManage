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
  public class VehicleEmissionTestController : ControllerBase
  {
    private readonly IVehicleEmissionTestService _vehicleEmissionTestService;
    private readonly IIdentityService identityService;

    public VehicleEmissionTestController(IVehicleEmissionTestService vehicleEmissionTestService, IIdentityService identityService)
    {
      this._vehicleEmissionTestService = vehicleEmissionTestService;
      this.identityService = identityService;
    }

    // GET api/VehicleEmissionTest/15/2
    [HttpGet]
    [Route("getAllVehicleEmissionTest/{vehicleId}")]
    public ActionResult Get(int vehicleId)
    {
      var response = _vehicleEmissionTestService.GetAllVehicleEmissionTest(vehicleId);
      return Ok(response);
    }

    // GET api/VehicleEmissionTest/5
    [HttpGet]
    [Route("getVehicleEmissionTestById/{id}")]
    public ActionResult GetVehicleEmissionTestById(long id)
    {
      var response = _vehicleEmissionTestService.GetVehicleEmissionTestById(id);
      return Ok(response);
    }

    // POST api/VehicleEmissionTest
    [HttpPost]
    [Route("saveVehicleEmissionTest")]
    public async Task<ActionResult> SaveVehicleEmissionTest([FromBody] VehicleEmissionTestViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleEmissionTestService.SaveVehicleEmissionTest(vm, userName);
      return Ok(response);
    }



    // DELETE api/VehicleEmissionTest/5
    [HttpDelete]
    [Route("deleteVehicleEmissionTest/{id}")]
    public async Task<ActionResult> DeleteVehicleEmissionTest(int id)
    {
      var userName = identityService.GetUserName();
      var response = await _vehicleEmissionTestService.DeleteVehicleEmissionTest(id, userName);
      return Ok(response);
    }


    [HttpGet]
    [Route("GetLatestRecordForVehicle/{vehicleId}")]
    public ActionResult GetLatestRecordForVehicle(long vehicleId)
    {
      var response = _vehicleEmissionTestService.GetLatestRecordForVehicle(vehicleId);
      return Ok(response);
    }

    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadEmissionTestImage")]
    public async Task<IActionResult> UploadEmissionTestImage()
    {
      var userName = identityService.GetUserName();

      var container = new FileContainerModel();

      var request = await Request.ReadFormAsync();

      container.Id = int.Parse(request["id"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var response = await _vehicleEmissionTestService.UploadEmissionTestImage(container, userName);

      return Ok(response);
    }


    [HttpGet]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadEmissionTestImage/{id:int}")]
    [ProducesResponseType(typeof(DownloadFileViewModel), (int)HttpStatusCode.OK)]
    public FileStreamResult DownloadEmissionTestImage(int id)
    {
      var response = _vehicleEmissionTestService.DownloadEmissionTestImage(id);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }
}
