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
    public class VehicleEngineOilMilageController : ControllerBase
    {
        private readonly IVehicleEngineOilMilageService _vehicleEngineOilMilageService;

        public VehicleEngineOilMilageController(IVehicleEngineOilMilageService vehicleEngineOilMilageService)
        {
            this._vehicleEngineOilMilageService = vehicleEngineOilMilageService;
        }

        // GET api/VehicleEngineOilMilage/15/2
        [HttpGet("{vehicleId:int}")]
        public ActionResult Get(int vehicleId)
        {
            var response = _vehicleEngineOilMilageService.GetAllVehicleEngineOilMilage(vehicleId);
            return Ok(response);
        }

        // GET api/VehicleEngineOilMilage/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleEngineOilMilageService.GetVehicleEngineOilMilageById(id);
            return Ok(response);
        }

        // POST api/VehicleEngineOilMilage
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleEngineOilMilageViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleEngineOilMilageService.SaveVehicleEngineOilMilage(vm, userName);
            return Ok(response);
        }



        // DELETE api/VehicleEngineOilMilage/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleEngineOilMilageService.DeleteVehicleEngineOilMilage(id, userName);
            return Ok(response);
        }

        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleEngineOilMilageService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
