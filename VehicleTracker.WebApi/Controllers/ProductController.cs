using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IProductService productService;
    private readonly IIdentityService identityService;
    public ProductController(IProductService productService, IIdentityService identityService)
    {
      this.productService = productService;
      this.identityService = identityService;
    }

    // GET api/Route/15/2
    [HttpGet]
    [Route("getAllProductSubCategories/{subCategoryId}")]
    public ActionResult GetAllProductSubCategories(int subCategoryId)
    {
      var response = productService.GetAllProducts(subCategoryId);
      return Ok(response);
    }

    // GET api/Route/5
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
      var response = productService.GetProductById(id);
      return Ok(response);
    }

    // POST api/Route
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await productService.SaveProduct(vm, userName);

      return Ok(response);
    }


    // DELETE api/Route/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var userName = identityService.GetUserName();
      var response = await productService.DeleteProduct(id, userName);
      return Ok(response);
    }

    [HttpGet]
    [Route("getProductSubCategories/{subCategoryId}")]
    public ActionResult GetProductSubCategories(int subCategoryId)
    {
      var response = productService.GetProductSubCategories(subCategoryId);
      return Ok(response);
    }
  }
}
