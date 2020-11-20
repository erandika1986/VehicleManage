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
    public class VehicleEmissionTestController : ControllerBase
    {
        private readonly IVehicleEmissionTestService _vehicleEmissionTestService;

        public VehicleEmissionTestController(IVehicleEmissionTestService vehicleEmissionTestService)
        {
            this._vehicleEmissionTestService = vehicleEmissionTestService;
        }

        // GET api/VehicleEmissionTest/15/2
        [HttpGet("{vehicleId:int}/{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int vehicleId, int pageSize, int currentPage)
        {
            var response = _vehicleEmissionTestService.GetAllVehicleEmissionTest(vehicleId, pageSize, currentPage);
            return Ok(response);
        }

        // GET api/VehicleEmissionTest/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleEmissionTestService.GetVehicleEmissionTestById(id);
            return Ok(response);
        }

        // POST api/VehicleEmissionTest
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleEmissionTestViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleEmissionTestService.AddNewVehicleEmissionTest(vm, userName);
            return Ok(response);
        }



        // DELETE api/VehicleEmissionTest/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleEmissionTestService.DeleteVehicleEmissionTest(id, userName);
            return Ok(response);
        }


        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleEmissionTestService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
