using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DropDownServiceController : ControllerBase
  {
    private readonly IDropDownService dropDownService;

    public DropDownServiceController(IDropDownService dropDownService)
    {
      this.dropDownService = dropDownService;
    }

    [HttpGet]
    [Route("getProductSubCategories/{categoryId}")]
    public ActionResult GetProductSubCategories(int categoryId)
    {
      var response = dropDownService.GetProductSubCategories(categoryId);
      return Ok(response);
    }

    [HttpGet]
    [Route("getProducts/{subCategoryId}")]
    public ActionResult GetProducts(int subCategoryId)
    {
      var response = dropDownService.GetProducts(subCategoryId);
      return Ok(response);
    }

    [HttpGet]
    [Route("getProductsForSupplier/{subCategoryId}/{supplierId}")]
    public ActionResult GetProductsForSupplier(int subCategoryId, int supplierId)
    {
      var response = dropDownService.GetProductsForSupplier(subCategoryId, supplierId);
      return Ok(response);
    }
  }
}
