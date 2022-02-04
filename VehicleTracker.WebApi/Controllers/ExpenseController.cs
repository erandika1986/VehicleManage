using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.Expenses;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService expenseService;
        private readonly IIdentityService identityService;

        public ExpenseController(IExpenseService expenseService, IIdentityService identityService)
        {
            this.expenseService = expenseService;
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("saveExpense")]
        public async Task<IActionResult> SaveExpense(ExpensesViewModel vm)
        {
            var userName = identityService.GetUserName();

            var response = await expenseService.SaveExpenses(vm, userName);

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetId(int id)
        {
            return Ok(id);
        }
    }
}
