using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class WarehouseController : ControllerBase
  {
    private readonly IWarehouseService _warehouseService;
    private readonly IIdentityService _identityService;


    public WarehouseController(IWarehouseService warehouseService, IIdentityService identityService)
    {
      this._warehouseService = warehouseService;
      this._identityService = identityService;
    }



  }
}
