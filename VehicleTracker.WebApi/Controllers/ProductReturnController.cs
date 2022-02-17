using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.ProductReturn;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReturnController : ControllerBase
    {
        private readonly IProductReturnService productReturnService;
        private readonly IIdentityService identityService;
        private readonly ILoggedInUserService loggedInUserService;

        public ProductReturnController(IProductReturnService productReturnService, 
            IIdentityService identityService, ILoggedInUserService loggedInUserService)
        {
            this.productReturnService = productReturnService;
            this.identityService = identityService;
            this.loggedInUserService = loggedInUserService;
        }

        [HttpGet]
        [Route("getProductReturnMasterData")]
        public IActionResult GetProductReturnMasterData()
        {
            var response = productReturnService.GetProductReturnMasterData();
            return Ok(response);
        }


        [HttpPost]
        [Route("getAllVehicleReturnRecord")]
        public IActionResult GetAllVehicleReturnRecord(ProductReturnFilterViewModel filters)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());

            var response = productReturnService.GetAllVehicleReturnRecord(filters, loggedInUser);
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveProductReturn")]
        public async Task<IActionResult> SaveProductReturn(ProductReturnViewModel vm)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());

            var response = await productReturnService.SaveProductReturn(vm, loggedInUser);
            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteProductReturn/{id}")]
        public async Task<IActionResult> DeleteProductReturn(int id)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());

            var response = await productReturnService.DeleteProductReturn(id, loggedInUser);
            return Ok(response);
        }

        [HttpGet]
        [Route("getSalesOrderListForSelectedClient/{clientId}")]
        public IActionResult GetSalesOrderListForSelectedClient(int clientId)
        {
            var response = productReturnService.GetSalesOrderListForSelectedClient(clientId);
            return Ok(response);
        }

        [HttpGet]
        [Route("getProductReturn/{id}")]
        public IActionResult GetProductReturn(int id)
        {
            var response =  productReturnService.GetProductReturn(id);
            return Ok(response);
        }
    }
}
