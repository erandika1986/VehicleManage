
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
using VehicleTracker.ViewModel.Common.Enums;
using VehicleTracker.ViewModel.Expenses;

namespace VehicleTracker.Business
{
    public class ExpenseService : IExpenseService
    {
        #region Member variable
        private readonly VMDBContext _db;
        private readonly IConfiguration _config;
        private readonly ILogger<IExpenseService> _logger;
        private readonly IUserService userService;
        #endregion

        public ExpenseService(VMDBContext db, IConfiguration config, ILogger<IExpenseService> logger, IUserService userService)
        {
            this._db = db;
            this._config = config;
            this._logger = logger;
            this.userService = userService;
        }

        public PaginatedItemsViewModel<BasicExpenseDetailViewModel> GellAllExpeses(ExpenseFilterViewModel filters, string userName)
        {

            int totalRecordCount = 0;
            int totalPageCount = 0;

            filters.FromDate = new DateTime(filters.FromYear, filters.FromMonth, filters.FromDay, 0, 0, 0);
            filters.ToDate = new DateTime(filters.ToYear, filters.FromMonth, filters.ToDay, 0, 0, 0);

            var query = _db.Expenses.Where(x=> x.Date >= filters.FromDate && x.Date <= filters.ToDate).OrderBy(x=>x.Date);

            if(filters.ExpenseCategoryId > 0)
            {
                query.Where(x => x.ExpenseCategoryId == filters.ExpenseCategoryId).OrderBy(x => x.Date);
            }

            var data = new List<BasicExpenseDetailViewModel>();

            totalRecordCount = query.Count();

            totalPageCount = (int)Math.Ceiling((Convert.ToDecimal(totalRecordCount) / filters.PageSize));

            var pageData = query.Skip((filters.CurrentPage - 1) * filters.PageSize).Take(filters.PageSize).ToList();

            pageData.ForEach(expese =>
            {
                var vm = new BasicExpenseDetailViewModel()
                {
                    Id = expese.Id,
                    Description = expese.Description,
                    Date = expese.Date,
                    Amount = expese.Amount,
                    CreatedOn = expese.CreatedOn,
                    CreatedBy = expese.CreatedBy.Username,
                    UpdatedOn = expese.UpdatedOn,
                    UpdatedBy = expese.UpdatedBy.Username,
                };

                data.Add(vm);
            });

            var expeseDataSet = new PaginatedItemsViewModel<BasicExpenseDetailViewModel>(filters.CurrentPage, filters.PageSize, totalPageCount, totalRecordCount, data);

            return expeseDataSet;

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
                        ExpenseCategoryId = (int)vm.ExpenseCategoryId,
                        Description = vm.Description,
                        Date = expenseDate,
                        Amount = vm.Amount,
                        CreatedOn = DateTime.UtcNow,
                        CreatedById = loggedInUser.Id,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedById = loggedInUser.Id

                    };
                  

                    if(vm.ExpenseCategoryId == ExpenseCategoryTypes.VehicleExpenses)
                    {

                        expense.VehicleExpense  = new VehicleExpense()
                        {
                          
                            VehicleExpenseType = (int) vm.VehicleExpenseTypeId,
                            VehicleId = vm.VehicleId
                        };

                        
                    }

                    _db.Expenses.Add(expense);

                    response.IsSuccess = true;
                    response.Message = "Expenses has been Save SuccessFully";

                }
                else
                {
                    expense.ExpenseCategoryId = (int)vm.ExpenseCategoryId;
                    expense.Description = vm.Description;
                    expense.Date = expenseDate;
                    expense.Amount = vm.Amount;
                    expense.UpdatedOn = DateTime.UtcNow;
                    expense.UpdatedById = loggedInUser.Id;

                    _db.Expenses.Update(expense);

                   

                    if (vm.ExpenseCategoryId == ExpenseCategoryTypes.VehicleExpenses)
                    {
                        if(expense.VehicleExpense == null)
                        {
                            expense.VehicleExpense = new VehicleExpense()
                            {

                                VehicleExpenseType = (int)vm.VehicleExpenseTypeId,
                                VehicleId = vm.VehicleId
                            };
                        }
                        else
                        {
                            expense.VehicleExpense.VehicleExpenseType = (int)vm.VehicleExpenseTypeId;
                            expense.VehicleExpense.VehicleId = vm.VehicleId;
                        }
                        
                       
                        
                    }
                    else
                    {
                        if(expense.VehicleExpense != null)
                        {
                            _db.VehicleExpenses.Remove(expense.VehicleExpense);
                        }
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
