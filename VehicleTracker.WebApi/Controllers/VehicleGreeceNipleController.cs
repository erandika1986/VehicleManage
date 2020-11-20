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
    public class VehicleGreeceNipleController : ControllerBase
    {
        private readonly IVehicleGreeceNipleService _vehicleGreeceNipleService;

        public VehicleGreeceNipleController(IVehicleGreeceNipleService vehicleGreeceNipleService)
        {
            this._vehicleGreeceNipleService = vehicleGreeceNipleService;
        }

        // GET api/VehicleGreeceNiple/15/2
        [HttpGet("{vehicleId:int}/{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int vehicleId, int pageSize, int currentPage)
        {
            var response = _vehicleGreeceNipleService.GetAllVehicleGreeceNiple(vehicleId, pageSize, currentPage);
            return Ok(response);
        }

        // GET api/VehicleGreeceNiple/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleGreeceNipleService.GetVehicleGreeceNipleById(id);
            return Ok(response);
        }

        // POST api/VehicleGreeceNiple
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleGreeceNipleViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleGreeceNipleService.AddNewVehicleGreeceNiple(vm, userName);
            return Ok(response);
        }



        // DELETE api/VehicleGreeceNiple/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleGreeceNipleService.DeleteVehicleGreeceNiple(id, userName);
            return Ok(response);
        }


        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleGreeceNipleService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
