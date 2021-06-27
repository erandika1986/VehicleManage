using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Route;

namespace System
{
    public static class  RouteExtension
    {
        public static Route ToModel(this RouteViewModel vm, Route model = null)
        {
            if (model == null)
                model = new Route();

            model.Id = vm.Id;
            model.RouteCode = vm.RouteCode;
            model.Name = vm.Name;
            model.StartFrom = vm.StartFrom;
            model.EndFrom = vm.EndFrom;
            model.TotalDistance = vm.TotalDistance;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static RouteViewModel ToVm(this Route model, RouteViewModel vm = null)
        {
            if (vm == null)
                vm = new RouteViewModel();

            vm.Id = model.Id;
            vm.Name = model.Name;
            vm.RouteCode = model.RouteCode;
            vm.StartFrom = model.StartFrom;
            vm.EndFrom = model.EndFrom;
            vm.TotalDistance = model.TotalDistance;
            vm.IsActive = model.IsActive;

            return vm;
        }
    }
}
