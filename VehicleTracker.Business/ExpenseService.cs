
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
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

        public async Task<ResponseViewModel> DeleteExpese(long id)
        {
            var response = new ResponseViewModel();

            try
            {
                 var expense = _db.Expenses.Where(x => x.Id == id).FirstOrDefault();

                expense.IsActive = false;

                _db.Expenses.Update(expense);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Expense Has Been Delete Successfully";


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error Orrered Please Try Again";
            }

            return response;
        }

        public PaginatedItemsViewModel<BasicExpenseDetailViewModel> GellAllExpeses(ExpenseFilterViewModel filters)
        {

            int totalRecordCount = 0;
            int totalPageCount = 0;

            filters.FromDate = new DateTime(filters.FromYear, filters.FromMonth, filters.FromDay, 0, 0, 0);
            filters.ToDate = new DateTime(filters.ToYear, filters.FromMonth, filters.ToDay, 0, 0, 0);

            var query = _db.Expenses.Where(x=> x.Date >= filters.FromDate && x.Date <= filters.ToDate && x.IsActive == true).OrderBy(x=>x.Date);

            if(filters.SelectedExpenseCategoryId > 0)
            {
                query.Where(x => x.ExpenseCategoryId == filters.SelectedExpenseCategoryId).OrderBy(x => x.Date);
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
                    ExpenseCategoryId = expese.ExpenseCategoryId,
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

        public ExpensesViewModel GetExpenseById(int id, int expenseCategoryId)
        {
            var response = new ExpensesViewModel();

            try
            {

              
                if(expenseCategoryId == (int)ExpenseCategoryTypes.VehicleExpenses)
                {
                    var query = (from expense in _db.Expenses
                                 join vehicleExpense in _db.VehicleExpenses on expense.Id equals vehicleExpense.Id
                                 where expense.Id == id && expense.ExpenseCategoryId == expenseCategoryId
                                 select new
                                 {
                                     expense.Id,
                                     expense.Description,
                                     expense.Date,
                                     expense.Amount,
                                     expense.ExpenseCategoryId,
                                     vehicleExpense.VehicleId,
                                     vehicleExpense.VehicleExpenseType

                                 }).FirstOrDefault();



                    response.Id = query.Id;
                    response.Description = query.Description;
                    response.Amount = query.Amount;
                    response.ExpenseDate = query.Date;
                    response.ExpenseCategoryId = (ExpenseCategoryTypes)query.ExpenseCategoryId;
                    response.VehicleId = query.VehicleId != 0? query.VehicleId :0;
                    response.VehicleExpenseTypeId = (VehicleExpensesTypes)query.VehicleExpenseType;

                }
                else
                {
                    var query = _db.Expenses.Where(x => x.Id == id && x.ExpenseCategoryId == expenseCategoryId).FirstOrDefault();
                    response.Id = query.Id;
                    response.Description = query.Description;
                    response.Amount = query.Amount;
                    response.ExpenseDate = query.Date;
                    response.ExpenseCategoryId = (ExpenseCategoryTypes)query.ExpenseCategoryId;

                }

            }
            catch(Exception ex)
            {
                
            }

            return response;
        }

        public ExpensesMasterDataViewModel GetExpensesMasterData()
        {
            var response = new ExpensesMasterDataViewModel();

            response.Vehicles = _db.Vehicles.Where(x => x.IsActive == true).Select(v => new DropDownViewModal() { Id = v.Id, Name = v.RegistrationNo }).ToList();

            foreach (ExpenseCategoryTypes expenses in (ExpenseCategoryTypes[])Enum.GetValues(typeof(ExpenseCategoryTypes)))
            {
                response.ExpensesCategories.Add(new DropDownViewModal() { Id = (int)expenses, Name = EnumHelper.GetEnumDescription(expenses) });
            }

            foreach (VehicleExpensesTypes vehicleExpenses in (VehicleExpensesTypes[])Enum.GetValues(typeof(VehicleExpensesTypes)))
            {
                response.VehicleExpenses.Add(new DropDownViewModal() { Id = (int)vehicleExpenses, Name = EnumHelper.GetEnumDescription(vehicleExpenses) });
            }

            return response;
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
                    expense = vm.ToNewModel();
                    expense.Date = expenseDate;
                    expense.CreatedById = loggedInUser.Id;
                    expense.UpdatedById = loggedInUser.Id;
                    
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

        public async Task<ResponseViewModel> UploadExpenseReceiptImage(FileContainerModel container)
        {
            var response = new ResponseViewModel();

            try
            {
                var expense = _db.Expenses.Where(x => x.Id == container.Id).FirstOrDefault();
                var folderPath = GetExpenseImageFolderPath(expense, _config);
                var firstFile = container.Files.FirstOrDefault();

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                if (firstFile != null && firstFile.Length > 0)
                {
                    var fileName = GetExpenseImageName(expense, Path.GetExtension(firstFile.FileName));
                    var filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await firstFile.CopyToAsync(stream);
                        var expenseImage = new ExpenseImage()
                        {
                            AttachementName = fileName,
                            Attachment = filePath
                         
                        };

                        expense.ExpenseImages.Add(expenseImage);
                        response.Message = "Expense image has been uploaded succesfully";
                    }
                }

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            { 
                response.IsSuccess = false;
                response.Message = "Image upload has been failed,Please try again";
            }


            return response;
        }

        #region Private Methods
        private string GetExpenseImageFolderPath(Expense model, IConfiguration config)
        {
            return string.Format(@"{0}{1}\{2}", config.GetSection("FileUploadPath").Value, FolderNames.EXPENSES, model.Id);
        }

        public static string GetExpenseImageName(Expense model, string extension)
        {
            return string.Format(@"Expense-Image-{0}{1}", model.Id, extension);
        }

        #endregion
    }


}
