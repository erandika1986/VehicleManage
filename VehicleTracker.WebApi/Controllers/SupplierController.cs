using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.Supplier;

namespace VehicleTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;


        public SupplierController(ISupplierService supplierService)
        {
            this._supplierService = supplierService;
        }


        // GET api/Route/15/2
        [HttpGet("{pageSize:int}/{currentPage:int}")]
        public ActionResult Get(int pageSize, int currentPage)
        {
            var response = _supplierService.GetAllSuppliers(pageSize, currentPage);
            return Ok(response);
        }

        // GET api/Route/5
        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var response = _supplierService.GetSupplierById(id);
            return Ok(response);
        }

        // POST api/Route
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SupplierViewModel vm)
        {
            var response = await _supplierService.SaveSupplier(vm);
            return Ok(response);
        }



        // DELETE api/Route/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _supplierService.DeleteSupplier(id);
            return Ok(response);
        }
    }
}
