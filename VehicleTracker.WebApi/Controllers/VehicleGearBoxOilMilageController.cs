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
    public class VehicleGearBoxOilMilageController : ControllerBase
    {
        private readonly IVehicleGearBoxOilMilageService _vehicleGearBoxOilMilageService;

        public VehicleGearBoxOilMilageController(IVehicleGearBoxOilMilageService vehicleGearBoxOilMilageService)
        {
            this._vehicleGearBoxOilMilageService = vehicleGearBoxOilMilageService;
        }

        // GET api/VehicleGearBoxOilMilage/15/2
        [HttpGet("{vehicleId:int}/{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int vehicleId, int pageSize, int currentPage)
        {
            var response = _vehicleGearBoxOilMilageService.GetAllVehicleGearBoxOilMilage(vehicleId, pageSize, currentPage);
            return Ok(response);
        }

        // GET api/VehicleGearBoxOilMilage/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleGearBoxOilMilageService.GetVehicleGearBoxOilMilageById(id);
            return Ok(response);
        }

        // POST api/VehicleGearBoxOilMilage
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleGearBoxOilMilageViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleGearBoxOilMilageService.AddNewVehicleGearBoxOilMilage(vm, userName);
            return Ok(response);
        }



        // DELETE api/VehicleGearBoxOilMilage/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleGearBoxOilMilageService.DeleteVehicleGearBoxOilMilage(id, userName);
            return Ok(response);
        }

        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleGearBoxOilMilageService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
