using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MasterDataController : ControllerBase
  {
    private readonly IMasterDataCodeSevice masterDataSevice;
    private readonly IIdentityService identityService;
    public MasterDataController(IMasterDataCodeSevice masterDataSevice, IIdentityService identityService)
    {
      this.masterDataSevice = masterDataSevice;
      this.identityService = identityService;
    }


    [HttpGet("getAllCodeTypes")]
    [ProducesResponseType(typeof(List<DropDownViewModal>), (int)HttpStatusCode.OK)]
    public IActionResult GetAllCodeTypes()
    {
      var response = masterDataSevice.GetAllCodeTypes();

      return Ok(response);
    }

    [HttpGet("getAllCodesForSelectedCodeType/{type:int}")]
    [ProducesResponseType(typeof(List<CodeViewModel>), (int)HttpStatusCode.OK)]
    public IActionResult GetAllCodesForSelectedCodeType(int type)
    {

      var response = masterDataSevice.GetAllCodesForSelectedCodeType((Codes)type);

      return Ok(response);
    }

    [HttpPost("saveCode")]
    [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SaveCode([FromBody] CodeViewModel vm)
    {
      var response = await masterDataSevice.SaveCode(vm);

      return Ok(response);
    }

    [HttpPost("deleteCode")]
    [ProducesResponseType(typeof(ResponseViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCode([FromBody] CodeViewModel vm)
    {
      var response = await masterDataSevice.DeleteCode(vm);

      return Ok(response);
    }
  }
}
