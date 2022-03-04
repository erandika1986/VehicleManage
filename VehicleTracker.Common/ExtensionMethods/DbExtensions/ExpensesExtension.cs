using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Expenses;

namespace System
{
    public static class ExpensesExtension
    {
        public static Expense ToNewModel(this ExpensesViewModel vm, Expense model = null)
        {
            if (model == null)
                model = new Expense();

            model.Id = vm.Id;
            model.ExpenseCategoryId = (int)vm.ExpenseCategoryId;
            model.Description = vm.Description;
            model.Amount = vm.Amount;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
  
            return model;
        }

        public static string GetExpenseFolderPath(this Expense model, IConfiguration config)
        {
            return string.Format(@"{0}{1}\{2}", config.GetSection("FileUploadPath").Value, FolderNames.EXPENSES, model.Id);
        }
    }
}
