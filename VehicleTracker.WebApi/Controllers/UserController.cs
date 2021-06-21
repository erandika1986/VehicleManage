using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VehicleTracker.Business;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Users;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly IIdentityService _identityService;
    public UserController(IUserService userService, IIdentityService identityService)
    {
      this._userService = userService;
      this._identityService = identityService;
    }


    [HttpPost]
    [Route("saveVehicle")]
    public async Task<IActionResult> SaveUser(UserViewModel vm)
    {
      var response = await _userService.SaveUser(vm);

      return Ok(response);
    }


    [HttpDelete]
    [Route("deleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
      var response = await _userService.DeleteUser(id);

      return Ok(response);
    }


    [HttpGet]
    [Route("getUserMasterData")]
    public IActionResult GetUserMasterData()
    {
      var response =  _userService.GetUserMasterData();

      return Ok(response);
    }


    [HttpGet]
    [Route("getAllUsers/{roleId}/{status}")]
    public IActionResult  GetAllUsers(int roleId, int status)
    {
      var response = _userService.GetAllUsers(roleId, status);

      return Ok(response);
    }

    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadUserImage")]
    public async Task<IActionResult> UploadUserImage()
    {
      var userName = _identityService.GetUserName();

      var container = new FileContainerModel();

      var request = await Request.ReadFormAsync();

      container.Id = int.Parse(request["id"]);
      container.Id = int.Parse(request["type"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var response = await _userService.UploadUserImage(container, userName);

      return Ok(response);
    }

    [HttpGet]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadUserImage/{id:int}/{type}")]
    [ProducesResponseType(typeof(DownloadFileViewModel), (int)HttpStatusCode.OK)]
    public FileStreamResult DownloadUserImage(int id,int type)
    {
      var response = _userService.DownloadUserImage(id,type);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }
}
