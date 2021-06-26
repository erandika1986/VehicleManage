using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Customer;
using VehicleTracker.WebApi.Helpers;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly ICustomerService _customerService;
    private readonly IIdentityService identityService;
    public CustomerController(ICustomerService customerService, IIdentityService identityService)
    {
      this._customerService = customerService;
      this.identityService = identityService;
    }

    [HttpGet]
    public ActionResult Get()
    {
      var response = _customerService.GetAllCustomers();
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
      var userName = identityService.GetUserName();

      var response = await _customerService.SaveCustomer(vm, userName);
      return Ok(response);
    }

    // DELETE api/Route/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var userName = identityService.GetUserName();

      var response = await _customerService.DeleteCustomer(id, userName);
      return Ok(response);
    }

        [HttpGet]
        [Route("getCustomerMasterData")]
        public ActionResult GetCustomerMasterData()
        {
            CustomerMasterDataViewModel response = _customerService.GetCustomerMasterData();

            return Ok(response);
        }
  }
}
