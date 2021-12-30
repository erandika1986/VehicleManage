using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.SalesOrder;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {

        private readonly ISaleOrderService saleOrderService;
        private readonly ILoggedInUserService loggedInUserService;
        private readonly IIdentityService identityService;

        public SalesOrderController(ISaleOrderService saleOrderService, ILoggedInUserService loggedInUserService, IIdentityService identityService)
        {
            this.saleOrderService = saleOrderService;
            this.loggedInUserService = loggedInUserService;
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("createNewSalesOrder")]
        public async Task<IActionResult> CreateNewSalesOrder()
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
            var response = await saleOrderService.CreateNewSalesOrder(loggedInUser);

            return Ok(response);
        }

        [HttpPost]
        [Route("getMySalesOrders")]
        public IActionResult GetMySalesOrders(SalesOrderFilter filters)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
            var response = saleOrderService.GetMySalesOrders(filters, loggedInUser);

            return Ok(response);
        }

        [HttpPost]
        [Route("getAllSalesOrders")]
        public IActionResult GetAllSalesOrders(SalesOrderFilter filters)
        {
            var response = saleOrderService.GetAllSalesOrders(filters);

            return Ok(response);
        }

        [HttpPost]
        [Route("saveSalesOrder")]
        public async Task<IActionResult> SaveSalesOrder(SalesOrderViewModel vm)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
            var response = await saleOrderService.SaveSalesOrder(vm, loggedInUser);

            return Ok(response);
        }

        [HttpPost]
        [Route("saveSalesOrderStep1")]
        public async Task<IActionResult> SaveSalesOrderStep1(SalesOrderStep1ViewModel vm)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
            var response = await saleOrderService.SaveSalesOrderStep1(vm, loggedInUser);

            return Ok(response);
        }

        [HttpPost]
        [Route("saveSalesOrderStep3")]
        public async Task<IActionResult> SaveSalesOrderStep3(SalesOrderStep3ViewModel vm)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
            var response = await saleOrderService.SaveSalesOrderStep3(vm, loggedInUser);

            return Ok(response);
        }

        [HttpGet]
        [Route("getSalesOrderById/{id}")]
        public IActionResult GetSalesOrderById(long id)
        {
            var response = saleOrderService.GetSalesOrderById(id);

            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteSalesOrder/{id:int}")]
        public async Task<IActionResult> DeleteSalesOrder(int id)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());
            var response = await saleOrderService.DeleteSalesOrder(id, loggedInUser);

            return Ok(response);
        }

        [HttpGet]
        [Route("getSalesOrderMasterData")]
        public IActionResult GetSalesOrderMasterData()
        {
            var response = saleOrderService.GetSalesOrderMasterData();

            return Ok(response);
        }

        [HttpGet]
        [Route("getCustomersByRouteId/{id:int}")]
        public IActionResult GetCustomersByRouteId(int id)
        {
            var response = saleOrderService.GetCustomersByRouteId(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("getWarehouseProductAvailability/{productId:int}/{salesOrderId:int}")]
        public IActionResult GetWarehouseProductAvailability(int productId, int salesOrderId)
        {
            var response = saleOrderService.GetWarehouseProductAvailability(productId, salesOrderId);

            return Ok(response);
        }

        [HttpPost]
        [Route("addProductToSalesOrder")]
        public async Task<IActionResult> AddProductToSalesOrder(SalesOrderProduct productDetail)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());

            var response = await saleOrderService.AddProductToSalesOrder(productDetail, loggedInUser);

            return Ok(response);
        }


        [HttpPost]
        [Route("deleteSingleProductRoSalesOrder")]
        public async Task<IActionResult> DeleteSingleProductRoSalesOrder(SalesOrderProduct productDetail)
        {
            var loggedInUser = loggedInUserService.GetLoggedInUserByUserName(identityService.GetUserName());

            var response = await saleOrderService.DeleteSingleProductRoSalesOrder(productDetail, loggedInUser);

            return Ok(response);
        }


        [HttpDelete]
        [Route("deleteAllProductFromSalesOrder/{productId}/{salesOrderId}")]
        public async Task<IActionResult> DeleteAllProductFromSalesOrder(int productId, int salesOrderId)
        {
            var response = await saleOrderService.DeleteAllProductFromSalesOrder(productId, salesOrderId);

            return Ok(response);
        }

        [HttpGet]
        [Route("getSalesOrderNumber")]
        public async Task<ActionResult> GetSalesOrderNumber()
        {
            var response = await saleOrderService.GetSalesOrderNumber();
            return Ok(response);
        }
    }
}
