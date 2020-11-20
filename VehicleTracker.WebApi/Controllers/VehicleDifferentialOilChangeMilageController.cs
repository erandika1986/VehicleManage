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
    public class VehicleDifferentialOilChangeMilageController : ControllerBase
    {
        private readonly IVehicleDifferentialOilChangeMilageService _vehicleDifferentialOilChangeMilageService;

        public VehicleDifferentialOilChangeMilageController(IVehicleDifferentialOilChangeMilageService vehicleDifferentialOilChangeMilageService)
        {
            this._vehicleDifferentialOilChangeMilageService = vehicleDifferentialOilChangeMilageService;
        }

        // GET api/VehicleDifferentialOilChangeMilage/15/2
        [HttpGet("{vehicleId:int}/{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int vehicleId,int pageSize, int currentPage)
        {
            var response = _vehicleDifferentialOilChangeMilageService.GetAllVehicleDifferentialOilChangeMilage(vehicleId, pageSize, currentPage);
            return Ok(response);
        }

        // GET api/VehicleDifferentialOilChangeMilage/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleDifferentialOilChangeMilageService.GetVehicleDifferentialOilChangeMilageById(id);
            return Ok(response);
        }

        // POST api/VehicleDifferentialOilChangeMilage
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleDifferentialOilChangeMilageViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleDifferentialOilChangeMilageService.AddNewVehicleDifferentialOilChangeMilage(vm, userName);
            return Ok(response);
        }



        // DELETE api/VehicleDifferentialOilChangeMilage/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleDifferentialOilChangeMilageService.DeleteVehicleDifferentialOilChangeMilage(id, userName);
            return Ok(response);
        }

        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleDifferentialOilChangeMilageService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
