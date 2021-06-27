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
  public class ProductCategoryController : ControllerBase
  {
    private readonly IProductCategoryService productCategoryService;
    private readonly IIdentityService identityService;

    public ProductCategoryController(IProductCategoryService productCategoryService, IIdentityService identityService)
    {
      this.productCategoryService = productCategoryService;
      this.identityService = identityService;
    }

    // GET api/Route/15/2
    [HttpGet]
    public ActionResult Get()
    {
      var response = productCategoryService.GetAllProductCategories();
      return Ok(response);
    }

    // GET api/Route/5
    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
      var response = productCategoryService.GetProductCategoryById(id);
      return Ok(response);
    }

    // POST api/Route
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductCategoryViewModel vm)
    {
      var userName = identityService.GetUserName();
      var response = await productCategoryService.SaveProductCategory(vm, userName);

      return Ok(response);
    }


    // DELETE api/Route/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      var userName = identityService.GetUserName();
      var response = await productCategoryService.DeleteProductCategory(id, userName);
      return Ok(response);
    }

    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    [Route("uploadProductCategoryImage")]
    public async Task<IActionResult> UploadFitnessReportImage()
    {
      var userName = identityService.GetUserName();

      var container = new FileContainerModel();

      var request = await Request.ReadFormAsync();

      container.Id = int.Parse(request["id"]);

      foreach (var file in request.Files)
      {
        container.Files.Add(file);
      }

      var response = await productCategoryService.UploadProductCategoryImage(container, userName);

      return Ok(response);
    }


    [HttpGet]
    [RequestSizeLimit(long.MaxValue)]
    [Route("downloadProductCategoryImage/{id:int}")]
    [ProducesResponseType(typeof(DownloadFileViewModel), (int)HttpStatusCode.OK)]
    public FileStreamResult DownloadFitnessReportImage(int id)
    {
      var response = productCategoryService.DownloadProductCategoryImage(id);

      return File(new MemoryStream(response.FileData), "application/octet-stream", response.FileName);
    }
  }
}
