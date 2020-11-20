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
    public class VehicleInsuranceController : ControllerBase
    {
        private readonly IVehicleInsuranceService _vehicleInsuranceService;

        public VehicleInsuranceController(IVehicleInsuranceService vehicleInsuranceService)
        {
            this._vehicleInsuranceService = vehicleInsuranceService;
        }

        // GET api/VehicleInsurance/15/2
        [HttpGet("{vehicleId:int}/{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int vehicleId, int pageSize, int currentPage)
        {
            var response = _vehicleInsuranceService.GetAllVehicleInsurance(vehicleId, pageSize, currentPage);
            return Ok(response);
        }

        // GET api/VehicleInsurance/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleInsuranceService.GetVehicleInsuranceById(id);
            return Ok(response);
        }

        // POST api/VehicleInsurance
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleInsuranceViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleInsuranceService.AddNewVehicleInsurance(vm, userName);
            return Ok(response);
        }


        // DELETE api/VehicleInsurance/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleInsuranceService.DeleteVehicleInsurance(id, userName);
            return Ok(response);
        }

        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleInsuranceService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
