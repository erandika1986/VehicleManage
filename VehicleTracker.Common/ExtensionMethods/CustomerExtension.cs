using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel;

namespace System
{
    public static class CustomerExtension
    {
        public static Client ToModel(this CustomerViewModel vm, Client model = null)
        {
            if (model == null)
                model = new Client();

            model.Id = vm.Id;
            model.Name = vm.Name;
            model.Description = vm.Description;
            model.ContactNo1 = vm.ContactNo1;
            model.ContactNo2 = vm.ContactNo2;
            model.Email = vm.Email;
            model.Address = vm.Address;
            model.Latitude = vm.Latitude;
            model.Priority = vm.Priority;
            model.RouteId = vm.RouteId;
            model.IsActive = true;

            return model;
        }

        public static CustomerViewModel ToVm(this Client model, CustomerViewModel vm = null)
        {
            if (vm == null)
                vm = new CustomerViewModel();

            vm.Id = model.Id;
            vm.Name = model.Name;
            vm.Description = model.Description;
            vm.ContactNo1 = model.ContactNo1;
            vm.ContactNo2 = model.ContactNo2;
            vm.Email = model.Email;
            vm.Address = model.Address;
            vm.Latitude = model.Latitude.HasValue?model.Latitude.Value:0;
            vm.Priority = model.Priority.HasValue?model.Priority.Value:0;
            vm.RouteId = model.RouteId.HasValue?model.RouteId.Value:(long)0;
            vm.IsActive = model.IsActive.Value;

            return vm;
        }
    }
}
