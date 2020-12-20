using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel;

namespace System
{
    public static class ProductExtension
    {
        public static Product ToModel(this ProductViewModel vm, Product model = null)
        {
            if (model == null)
                model = new Product();

            model.Id = vm.Id;
            model.ProductName = vm.Name;
            model.ProductCode = vm.Description;
            model.Picture = vm.Picture;
            model.UnitPrice = vm.UnitPrice;
            model.AvailableQty = vm.AvailableQty;
            model.SubProductCategoryId = vm.ProductSubCategoryId;
            model.SupplierId = vm.SupplierId;
            model.IsActive = vm.IsActive;

            return model;
        }

        public static ProductViewModel ToVm(this Product model, ProductViewModel vm = null)
        {
            if (vm == null)
                vm = new ProductViewModel();

            vm.Id = model.Id;
            vm.Name = model.ProductName;
            vm.Description =model.ProductCode ;
            vm.Picture= model.Picture  ;
            vm.UnitPrice= model.UnitPrice  ;
            vm.AvailableQty= model.AvailableQty  ;
            vm.ProductSubCategoryId = model.SubProductCategoryId  ;
            vm.SupplierId = model.SupplierId ;
            vm.IsActive = model.IsActive.Value ;

            return vm;
        }
    }
}
