using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.Inventory;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InventoryController : ControllerBase
  {
    private readonly IInventoryService inventoryService;
    private readonly IIdentityService identityService;

    public InventoryController(IInventoryService inventoryService, IIdentityService identityService)
    {
      this.inventoryService = inventoryService;
      this.identityService = identityService;
    }


    [HttpPost]
    [Route("getProductInvetorySummary")]
    public IActionResult GetProductInvetorySummary(InventoryFilter filter)
    {
      var response = inventoryService.GetProductInvetorySummary(filter);

      return Ok(response);
    }

    [HttpPost]
    [Route("addNewInventoryRecords")]
    public async Task<IActionResult> AddNewInventoryRecords(POInventoryReceievedDetail vm)
    {
      var userName = identityService.GetUserName();
      var response = await inventoryService.AddNewInventoryRecords(vm, userName);

      return Ok(response);
    }

    [HttpDelete]
    [Route("deleteInventory/{id:int}")]
    public async Task<IActionResult> DeleteInventory(int id)
    {
      var response = await inventoryService.DeleteInventory(id);

      return Ok(response);
    }

    [HttpGet]
    [Route("getInventoryDetailsForPO/{poId:int}")]
    public IActionResult GetInventoryDetailsForPO(int poId)
    {
      var response = inventoryService.GetInventoryDetailsForPO(poId);

      return Ok(response);
    }

    [HttpGet]
    [Route("getInventoryMasterData")]
    public ActionResult GetInventoryMasterData()
    {
      var response = inventoryService.GetInventoryMasterData();
      return Ok(response);
    }
  }
}
