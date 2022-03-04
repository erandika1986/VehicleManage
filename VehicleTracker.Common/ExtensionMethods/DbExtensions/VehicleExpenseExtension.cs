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


            return model;
        }

        public static VehicleExpenseViewModel ToVm(this VehicleExpense model, VehicleExpenseViewModel vm = null)
        {
            if (vm == null)
                vm = new VehicleExpenseViewModel();

            vm.Id = model.Id;


            return vm;
        }
    }
}
