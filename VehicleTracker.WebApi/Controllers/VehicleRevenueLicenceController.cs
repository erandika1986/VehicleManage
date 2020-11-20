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
    public class VehicleRevenueLicenceController : ControllerBase
    {
        private readonly IVehicleRevenueLicenceService _vehicleRevenueLicenceService;

        public VehicleRevenueLicenceController(IVehicleRevenueLicenceService vehicleRevenueLicenceService)
        {
            this._vehicleRevenueLicenceService = vehicleRevenueLicenceService;
        }

        // GET api/VehicleRevenueLicence/15/2
        [HttpGet("{vehicleId:int}/{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int vehicleId, int pageSize, int currentPage)
        {
            var response = _vehicleRevenueLicenceService.GetAllVehicleRevenueLicence(vehicleId, pageSize, currentPage);
            return Ok(response);
        }

        // GET api/VehicleRevenueLicence/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleRevenueLicenceService.GetVehicleRevenueLicenceById(id);
            return Ok(response);
        }

        // POST api/VehicleRevenueLicence
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleRevenueLicenceViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleRevenueLicenceService.AddNewVehicleRevenueLicence(vm, userName);
            return Ok(response);
        }


        // DELETE api/VehicleRevenueLicence/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleRevenueLicenceService.DeleteVehicleRevenueLicence(id, userName);
            return Ok(response);
        }

        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleRevenueLicenceService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
