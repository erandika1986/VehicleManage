using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(IVehicleTypeService vehicleTypeService)
        {
            this._vehicleTypeService = vehicleTypeService;
        }


        [HttpGet]
        [Route("getVehicleTypeMasterData")]
        public IActionResult GetVehicleTypeMasterData()
        {
            var response = _vehicleTypeService.GetVehicleTypeMasterData();

            return Ok(response);
        }

        [HttpPost]
        [Route("saveVehicleType")]
        public async Task<IActionResult> AddNewVehicalType(VehicleTypeViewModel vehicleTypeViewModel)
        {
            var response = await _vehicleTypeService.SaveVehicleType(vehicleTypeViewModel);
            return Ok(response);
        }


        [HttpDelete]
        [Route("deleteVehicleType/{id:int}")]
        public async Task<IActionResult> DeleteVehicleType(long id)
        {
            var response = await this._vehicleTypeService.DeleteVehicleType(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("getAllVehicleTypes")]
        public IActionResult GetAllVehicleTypes()
        {
            var response = this._vehicleTypeService.GetAllVehicleTypes();
            return Ok(response);
        }

        [HttpGet]
        [Route("getVehicleTypeById/{id:int}")]
        public IActionResult GetVehicleTypeById(long id)
        {
            var response = this._vehicleTypeService.GetVehicleTypeById(id);
            return Ok(response);
        }
    }
}
