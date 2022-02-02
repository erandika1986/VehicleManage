
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Expenses;

namespace VehicleTracker.Business
{
    public class ExpenseService : IExpenseService
    {
        #region Member variable
        private readonly VMDBContext _db;
        private readonly IConfiguration _config;
        private readonly IExpenseService _expenseService;
        private readonly ILogger<IInventoryService> _logger; 
        #endregion

        public ExpenseService(VMDBContext db, IConfiguration config, IExpenseService expenseService, ILogger<IInventoryService> logger)
        {
            this._db = db;
            this._config = config;
            this._expenseService = expenseService;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> SaveExpenses(ExpensesViewModel vm, string username)
        {
            var response = new ResponseViewModel();

            var loggedInUser = _db.Users.Where(x => x.Username == username).FirstOrDefault();
            
            if(vm.Id == 0)
            {
               
            }


            return response;
        }
    }
}
