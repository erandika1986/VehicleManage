using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PurchaseOrderController : ControllerBase
  {
    private readonly IPurchaseOrderService _purchaseOrderService;
    private readonly IIdentityService _identityService;
    public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IIdentityService identityService)
    {
      this._purchaseOrderService = purchaseOrderService;
      this._identityService = identityService;
    }

    // GET api/Route/15/2
    [HttpGet]
    public ActionResult Get()
    {
      var response = _purchaseOrderService.GetAllPurchaseOrder();
      return Ok(response);
    }

    // GET api/Route/5
    [HttpGet("{id}")]
    public ActionResult Get(long id)
    {
      var response = _supplierService.GetSupplierById(id);
      return Ok(response);
    }

    // POST api/Route
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] SupplierViewModel vm)
    {
      var username = identityService.GetUserName();
      var response = await _supplierService.SaveSupplier(vm, username);
      return Ok(response);
    }



    // DELETE api/Route/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var response = await _supplierService.DeleteSupplier(id);
      return Ok(response);
    }
  }
}
