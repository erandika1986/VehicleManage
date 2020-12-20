using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel;
using VehicleTracker.WebApi.Helpers;

namespace VehicleTracker.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet("{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int pageSize, int currentPage)
        {
            var response = _customerService.GetAllCustomers(pageSize, currentPage);
            return Ok(response);
        }

        // GET api/Route/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _customerService.GetCustomerById(id);
            return Ok(response);
        }

        // POST api/Route
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();

            var response = await _customerService.AddNewCustomer(vm, userName);
            return Ok(response);
        }

        // PUT api/Route
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CustomerViewModel vm)
        {
            var userName = IdentityHelper.GetUsername();

            var response = await _customerService.UpdateCustomer(vm, userName);
            return Ok(response);
        }

        // DELETE api/Route/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userName = IdentityHelper.GetUsername();

            var response = await _customerService.DeleteCustomer(id, userName);
            return Ok(response);
        }
    }
}
