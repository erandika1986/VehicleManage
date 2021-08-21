using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.SalesOrder;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SalesOrderController : ControllerBase
  {

    private readonly ISaleOrderService saleOrderService;
    private readonly ILoggedInUserService loggedInUserService;
    private readonly IIdentityService identityService;

    public SalesOrderController(ISaleOrderService saleOrderService, ILoggedInUserService loggedInUserService, IIdentityService identityService)
    {
      this.saleOrderService = saleOrderService;
      this.loggedInUserService = loggedInUserService;
      this.identityService = identityService;
    }


    [HttpPost]
    [Route("getMySalesOrders")]
    public IActionResult GetMySalesOrders(SalesOrderFilter filters)
    {
      var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
      var response = saleOrderService.GetMySalesOrders(filters, loggedInUser);

      return Ok(response);
    }

    [HttpPost]
    [Route("getAllSalesOrders")]
    public IActionResult GetAllSalesOrders(SalesOrderFilter filters)
    {
      var response = saleOrderService.GetAllSalesOrders(filters);

      return Ok(response);
    }

    [HttpPost]
    [Route("saveSalesOrder")]
    public async Task<IActionResult> SaveSalesOrder(SalesOrderViewModel vm)
    {
      var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
      var response = await saleOrderService.SaveSalesOrder(vm, loggedInUser);

      return Ok(response);
    }

    [HttpDelete]
    [Route("deleteSalesOrder/{id:int}")]
    public async Task<IActionResult> DeleteSalesOrder(int id)
    {
      var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
      var response = await saleOrderService.DeleteSalesOrder(id, loggedInUser);

      return Ok(response);
    }
  }
}
