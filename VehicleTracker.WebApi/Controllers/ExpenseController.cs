using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Expenses;
using VehicleTracker.WebApi.Infrastructure.Services;

namespace VehicleTracker.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IIdentityService identityService;

        public ExpenseController(IExpenseService _expenseService, IIdentityService identityService)
        {
            this._expenseService = _expenseService;
            this.identityService = identityService;
        }

        [HttpPost]
        [Route("saveExpese")]
        public async Task<IActionResult> SaveExpenses(ExpensesViewModel vm)
        {
            var username = identityService.GetUserName();
            var response = await _expenseService.SaveExpenses(vm, username);

            return Ok(response);
        }
    }
}
