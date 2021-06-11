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
    public class VehicleAirCleanerController : ControllerBase
    {
        private readonly IVehicleAirCleanerService _vehicleAirCleanerService;

        public VehicleAirCleanerController(IVehicleAirCleanerService vehicleAirCleanerService)
        {
            this._vehicleAirCleanerService = vehicleAirCleanerService;
        }

        // GET api/VehicleAirCleaner/15/2
        [HttpGet("{vehicleId:int}")]
        public ActionResult Get(int vehicleId)
        {
            var response = _vehicleAirCleanerService.GetAllVehicleAirCleaner(vehicleId);
            return Ok(response);
        }

        // GET api/VehicleAirCleaner/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleAirCleanerService.GetVehicleAirCleanerById(id);
            return Ok(response);
        }

        // POST api/VehicleAirCleaner
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleAirCleanerViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleAirCleanerService.SaveVehicleAirCleaner(vm, userName);
            return Ok(response);
        }



        // DELETE api/VehicleAirCleaner/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleAirCleanerService.DeleteVehicleAirCleaner(id, userName);
            return Ok(response);
        }

        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleAirCleanerService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
