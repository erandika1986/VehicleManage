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
    public class VehicleFitnessReportController : ControllerBase
    {
        private readonly IVehicleFitnessReportService _vehicleFitnessReportService;

        public VehicleFitnessReportController(IVehicleFitnessReportService vehicleFitnessReportService)
        {
            this._vehicleFitnessReportService = vehicleFitnessReportService;
        }

        // GET api/VehicleFitnessReport/15/2
        [HttpGet("{vehicleId:int}/{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int vehicleId, int pageSize, int currentPage)
        {
            var response = _vehicleFitnessReportService.GetAllVehicleFitnessReport(vehicleId, pageSize, currentPage);
            return Ok(response);
        }

        // GET api/VehicleFitnessReport/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleFitnessReportService.GetVehicleFitnessReportById(id);
            return Ok(response);
        }

        // POST api/VehicleFitnessReport
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleFitnessReportViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleFitnessReportService.AddNewVehicleFitnessReport(vm, userName);
            return Ok(response);
        }



        // DELETE api/VehicleFitnessReport/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleFitnessReportService.DeleteVehicleFitnessReport(id, userName);
            return Ok(response);
        }

        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleFitnessReportService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
