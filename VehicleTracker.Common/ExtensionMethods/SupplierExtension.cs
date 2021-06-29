using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Supplier;

namespace System
{
    public static class SupplierExtension
    {

        public static Supplier ToModel(this SupplierViewModel vm, Supplier model = null)
        {
            if (model == null)
                model = new Supplier();

            model.Id = vm.Id;
            model.Name = vm.Name;
            model.Description = vm.Description;
            model.Address = vm.Address;
            model.Phone1 = vm.Phone1;
            model.Phone2 = vm.Phone2;
            model.Email1 = vm.Email1;
            model.Email2 = vm.Email2;
            model.Bank = vm.Bank;
            model.AccountNo = vm.AccountNo;
            model.Branch = vm.Branch;
            model.BranchCode = vm.BranchCode;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static SupplierViewModel ToVm(this Supplier model, SupplierViewModel vm = null)
        {
            if (vm == null)
                vm = new SupplierViewModel();

            vm.Id = model.Id;
            vm.Name = model.Name;
            vm.Description = model.Description;
            vm.Address = model.Address;
            vm.Phone1 = model.Phone1;
            vm.Phone2 = model.Phone2;
            vm.Email1 = model.Email1;
            vm.Email2 = model.Email2;
            vm.Bank = model.Bank;
            vm.AccountNo = model.AccountNo;
            vm.Branch = model.Branch;
            vm.BranchCode = model.BranchCode;
            vm.IsActive = model.IsActive.Value;

            return vm;
        }
    }
}
