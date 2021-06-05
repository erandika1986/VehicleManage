using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;
using VehicleTracker.WebApi.Helpers;

namespace VehicleTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this._vehicleService = vehicleService;
        }



        //Vehical control methods
        [HttpGet]
        [Route("getVehicleMasterData")]
        public IActionResult GetVehicleMasterData()
        {
            var response = this._vehicleService.GetVehicleMasterData();
            return Ok(response);
        }

        [HttpPost]
        [Route("addNewVehicle")]
        public async Task<IActionResult> AddNewVehical(VehicleViewModel vehicleViewModel)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await this._vehicleService.AddNewVehicle(vehicleViewModel, userName);
            return Ok(response);
        }

        [HttpPut]
        [Route("updateVehicle")]
        public async Task<IActionResult> UpdateVehicle(VehicleViewModel vehicleViewModel)
        {
            var userName = IdentityHelper.GetUsername();
            var response = await this._vehicleService.UpdateVehicle(vehicleViewModel, userName);
            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteVehicle/{id:int}")]
        public async Task<IActionResult> DeleteVehicle(long id)
        {
            var response = await this._vehicleService.DeleteVehicle(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("getAllVehicles/{pageSize:int}/{currentPage:int}/{sortBy}/{sortDirection}/{searchText?}")]
        public IActionResult GetAllVehicles(int pageSize, int currentPage,string sortBy,string sortDirection,string searchText)
        {
            var response = this._vehicleService.GetAllVehicles(pageSize, currentPage, sortBy, sortDirection, searchText);
            return Ok(response);
        }

        [HttpGet]
        [Route("getVehicleById/{id:int}")]
        public IActionResult GetVehicleById(long id)
        {
            var response = this._vehicleService.GetVehicleById(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("isVehicleAlreadyExists/{regNo:minlength(0)}")]
        public IActionResult IsVehicleAlreadyExists(string regNo)
        {
            var response = _vehicleService.IsVehicleAlreadyExists(regNo);

            return Ok(response);
        }

    }
}
