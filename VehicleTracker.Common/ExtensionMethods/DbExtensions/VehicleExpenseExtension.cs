using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common.Enums;
using VehicleTracker.ViewModel.Vehicle;

namespace System
{
    public static class VehicleExpenseExtension
    {
        public static VehicleExpense ToModel(this VehicleExpenseViewModel vm, VehicleExpense model = null)
        {
            if (model == null)
                model = new VehicleExpense();

            model.Id = vm.Id;
            model.ExpenseType = (int)vm.ExpenseType;
            model.VehicleId = vm.VehicleId;
            model.Description = vm.Description;
            model.Date = vm.Date;
            model.Amount = vm.Amount;
            model.CreatedOn = vm.CreatedOn;
            model.CreatedBy = vm.CreatedBy;
            model.UpdatedOn = vm.UpdatedOn;
            model.UpdatedBy = vm.UpdatedBy;

            return model;
        }

        public static VehicleExpenseViewModel ToVm(this VehicleExpense model, VehicleExpenseViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleExpenseViewModel();

            vm.Id = model.Id;
            vm.ExpenseType = (ExpenseType)model.ExpenseType;
            vm.VehicleId = model.VehicleId;
            vm.Description = model.Description;
            vm.Date = model.Date;
            vm.Amount = model.Amount;
            vm.CreatedOn = model.CreatedOn;
            vm.CreatedBy = model.CreatedBy;
            vm.UpdatedOn = model.UpdatedOn;
            vm.UpdatedBy = model.UpdatedBy;

            return vm;
        }
    }
}
