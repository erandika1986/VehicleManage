using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.PurchaseOrder;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Authorize]
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


    [HttpGet]
    public ActionResult Get()
    {
      var response = _purchaseOrderService.GetAllPurchseOrder();
      return Ok(response);
    }


    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
      var username = _identityService.GetUserName();
      var response = _purchaseOrderService.GetPurchaseOrderById(id, username);
      return Ok(response);
    }


    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PurchaseOrderViewModel vm)
    {
      var username = _identityService.GetUserName();
      var response = await _purchaseOrderService.SavePurchaseOrder(vm, username);
      return Ok(response);
    }




    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var username = _identityService.GetUserName();
      var response = await _purchaseOrderService.DeletePurchaseOrder(id, username);
      return Ok(response);
    }


    [HttpGet]
    [Route("getPurchaseOrderMasterData")]
    public ActionResult GetPurchaseOrderMasterData()
    {
      var response = _purchaseOrderService.GetPurchaseOrderMasterData();
      return Ok(response);
    }

    [HttpGet]
    [Route("getPONumber")]
    public async Task<ActionResult> GetPONumber()
    {
      var response = await _purchaseOrderService.GetPONumber();
      return Ok(response);
    }
  }
}
