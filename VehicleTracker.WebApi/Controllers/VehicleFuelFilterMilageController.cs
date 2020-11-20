﻿using Microsoft.AspNetCore.Authorization;
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
    public class VehicleFuelFilterMilageController : ControllerBase
    {
        private readonly IVehicleFuelFilterMilageService _vehicleFuelFilterMilageService;

        public VehicleFuelFilterMilageController(IVehicleFuelFilterMilageService vehicleFuelFilterMilageService)
        {
            this._vehicleFuelFilterMilageService = vehicleFuelFilterMilageService;
        }

        // GET api/VehicleFuelFilterMilage/15/2
        [HttpGet("{vehicleId:int}/{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int vehicleId, int pageSize, int currentPage)
        {
            var response = _vehicleFuelFilterMilageService.GetAllVehicleFuelFilterMilage(vehicleId, pageSize, currentPage);
            return Ok(response);
        }

        // GET api/VehicleFuelFilterMilage/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _vehicleFuelFilterMilageService.GetVehicleFuelFilterMilageById(id);
            return Ok(response);
        }

        // POST api/VehicleFuelFilterMilage
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VehicleFuelFilterMilageViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleFuelFilterMilageService.AddNewVehicleFuelFilterMilage(vm, userName);
            return Ok(response);
        }


        // DELETE api/VehicleFuelFilterMilage/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await _vehicleFuelFilterMilageService.DeleteVehicleFuelFilterMilage(id, userName);
            return Ok(response);
        }

        [HttpGet("getLatestRecordForVehicle/{vehicleId:long}")]
        public ActionResult GetLatestRecordForVehicle(long vehicleId)
        {
            var response = _vehicleFuelFilterMilageService.GetLatestRecordForVehicle(vehicleId);
            return Ok(response);
        }
    }
}
