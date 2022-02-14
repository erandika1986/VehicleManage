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

        [HttpPost]
        [Route("getAllExpenses")]
        public ActionResult GellAllExpeses(ExpenseFilterViewModel filters)
        {
            var response = expenseService.GellAllExpeses(filters);

            return Ok(response);
        }

        [HttpGet]
        [Route("gelAllExpenses/{id:int}/{expenseCategoryId:int}")]
        public IActionResult getExpenseById(int id, int expenseCategoryId)
        {
            var response = expenseService.GetExpenseById(id, expenseCategoryId);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(long id)
        {
            var response = await expenseService.DeleteExpese(id);

            return Ok(response);
        }

        [HttpGet("getExpensesMasterData")]
        public ActionResult GetExpensesMasterData()
        {
            var response = expenseService.GetExpensesMasterData();
            return Ok(response);
        }

    }
}
