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
  public class ProductSubCategoryController : ControllerBase
  {
    private readonly IProductSubCategoryService productSubCategoryService;
    private readonly IIdentityService identityService;

    public ProductSubCategoryController(IProductSubCategoryService productSubCategoryService, IIdentityService identityService)
    {
      this.productSubCategoryService = productSubCategoryService;
      this.identityService = identityService;
    }

    // GET api/Route/15/2
    [HttpGet]
    [Route("getAllByCategoryId/{categoryId}")]
    public ActionResult GetAllProductSubCategories(int categoryId)
    {
      var response = productSubCategoryService.GetAllProductSubCategories(categoryId);
      return Ok(response);
    }

    // GET api/Route/5
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
      var response = productSubCategoryService.GetProductSubCategoryById(id);
      return Ok(response);
    }

    // POST api/Route
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductSubCategoryViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await productSubCategoryService.SaveProductSubCategory(vm, userName);

      return Ok(response);
    }


    // DELETE api/Route/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var userName = identityService.GetUserName();
      var response = await productSubCategoryService.DeleteProductSubCategory(id, userName);
      return Ok(response);
    }

    [HttpGet]
    [Route("getProductCategories")]
    public ActionResult GetProductCategories()
    {
      var response =  productSubCategoryService.GetProductCategories();
      return Ok(response);
    }
  }
}
