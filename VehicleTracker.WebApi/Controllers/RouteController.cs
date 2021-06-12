using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleTracker.Business;
using VehicleTracker.ViewModel.Route;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class RouteController : ControllerBase
  {
    private readonly IRouteService _routeService;
    private readonly IIdentityService identityService;

    public RouteController(IRouteService routeService, IIdentityService identityService)
    {
      this._routeService = routeService;
      this.identityService = identityService;
    }

    // GET api/Route/15/2
    [HttpGet]
    public ActionResult Get()
    {
      var response = _routeService.GetAllRoutes();
      return Ok(response);
    }

    // GET api/Route/5
    [HttpGet("{id}")]
    public ActionResult Get(long id)
    {
      var response = _routeService.GetRouteById(id);
      return Ok(response);
    }

    // POST api/Route
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] RouteViewModel vm)
    {
      var response = await _routeService.SaveRoute(vm);
      return Ok(response);
    }


    // DELETE api/Route/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var response = await _routeService.DeleteRoute(id);
      return Ok(response);
    }
  }
}
