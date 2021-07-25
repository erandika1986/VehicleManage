using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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


    [HttpPost]
    [Route("getAllPurchseOrder")]
    public ActionResult GetAllPurchseOrder(PurchaseOrderFilter filter)
    {
      var username = _identityService.GetUserName();
      var response = _purchaseOrderService.GetAllPurchseOrder(filter,username);
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


    [HttpGet]
    [Route("getProductSubCategories/{categoryId}")]
    public ActionResult GetProductSubCategories(int categoryId)
    {
      var response = _purchaseOrderService.GetProductSubCategories(categoryId);
      return Ok(response);
    }

    [HttpGet]
    [Route("getProducts/{subCategoryId}")]
    public ActionResult GetProducts(int subCategoryId)
    {
      var response = _purchaseOrderService.GetProducts(subCategoryId);
      return Ok(response);
    }

    [HttpGet]
    [Authorize]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadPurchasingOrderForm/{id:int}")]
    public FileStreamResult DownloadPurchasingOrderForm(int id)
    {
      var response = _purchaseOrderService.DownloadPurchasingOrderForm(id);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }
}
