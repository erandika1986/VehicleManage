using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleTracker.Business;
using VehicleTracker.ViewModel.Route;

namespace VehicleTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            this._routeService = routeService;
        }

        // GET api/Route/15/2
        [HttpGet("{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int pageSize, int currentPage)
        {
            var response = _routeService.GetAllRoutes(pageSize, currentPage);
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
            var response =await _routeService.AddNewRoute(vm);
            return Ok(response);
        }

        // PUT api/Route
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] RouteViewModel vm)
        {
            var response = await _routeService.UpdateRoute(vm);
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