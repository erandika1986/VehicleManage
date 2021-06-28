using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;
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

    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadSubProductCategoryImage")]
    public async Task<IActionResult> UploadSubProductCategoryImage()
    {
      var userName = identityService.GetUserName();

      var container = new FileContainerModel();

      var request = await Request.ReadFormAsync();

      container.Id = int.Parse(request["id"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var response = await productSubCategoryService.UploadSubProductCategoryImage(container, userName);

      return Ok(response);
    }


    [HttpGet]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadProductSubCategoryImage/{id:int}")]
    [ProducesResponseType(typeof(DownloadFileViewModel), (int)HttpStatusCode.OK)]
    public FileStreamResult DownloadProductSubCategoryImage(int id)
    {
      var response = productSubCategoryService.DownloadProductSubCategoryImage(id);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }
}
