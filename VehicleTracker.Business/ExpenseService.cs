
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
            try
            {
                var loggedInUser = _db.Users.Where(x => x.Username == username).FirstOrDefault();
                var expense = _db.Expenses.FirstOrDefault(x => x.Id == vm.Id);

                var expenseDate = new DateTime(vm.ExpenseYear, vm.ExpenseMonth, vm.ExpenseDay);

                if (expense == null)
                {
                    expense = new Expense()
                    {
                        Id = vm.Id,
                        ExpenseCategoryId = vm.ExpenseCategoryId,
                        Description = vm.Description,
                        Date = expenseDate,
                        Amount = vm.Amount,
                        CreatedOn = DateTime.UtcNow,
                        CreatedById = loggedInUser.Id,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedById = loggedInUser.Id

                    };
                    _db.Expenses.Add(expense);

                    if(vm.VehicleId != 0)
                    {
                        var vehicleExpense = new VehicleExpense()
                        {
                            Id = vm.Id,
                            VehicleExpenseType = vm.VehicleExpenseTypeId,
                            VehicleId = vm.VehicleId
                        };

                        _db.VehicleExpenses.Add(vehicleExpense);
                    }

                    response.IsSuccess = true;
                    response.Message = "Expenses has been Save SuccessFully";

                }
                else
                {
                    expense.ExpenseCategoryId = vm.ExpenseCategoryId;
                    expense.Description = vm.Description;
                    expense.Date = expenseDate;
                    expense.Amount = vm.Amount;
                    expense.UpdatedOn = DateTime.UtcNow;
                    expense.UpdatedById = loggedInUser.Id;

                    _db.Expenses.Update(expense);

                    var vehicleExpense = _db.VehicleExpenses.Where(x => x.Id == vm.Id).FirstOrDefault();

                    if (vehicleExpense != null)
                    {
                        vehicleExpense.VehicleExpenseType = vm.VehicleExpenseTypeId;
                        vehicleExpense.VehicleId = vm.VehicleId;
                       
                        _db.VehicleExpenses.Update(vehicleExpense);
                    }

                    response.IsSuccess = true;
                    response.Message = "Expenses has been Update SuccessFully";


                }

                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has benn orrcured Plase try again";
            }
           
            return response;
        }
    }
}
