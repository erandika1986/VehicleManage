using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Expenses;

namespace VehicleTracker.Business.Interfaces
{
    public interface IExpenseService
    {
        Task<ResponseViewModel> SaveExpenses(ExpensesViewModel vm, string username);
        PaginatedItemsViewModel<BasicExpenseDetailViewModel> GellAllExpeses(ExpenseFilterViewModel filters, string userName);


    }
}
