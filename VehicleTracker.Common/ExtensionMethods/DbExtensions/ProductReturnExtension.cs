using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Common;
using VehicleTracker.Model;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel.ProductReturn;

namespace System
{
    public static class ProductReturnExtension
    {
        public static ProductReturn ToModel(this ProductReturnViewModel vm, ProductReturn model=null)
        {
            if(model==null)
            {
                model = new ProductReturn();
            }

            model.ProductId = vm.SelectedProductId;
            model.ClientId = vm.SelectedClientId;
            model.SaleOrderId = vm.SelectedSaleOrderId > 0 ? vm.SelectedSaleOrderId : (long?)null;
            model.Qty = vm.Qty;
            model.ReturnDate = vm.ReturnDate;
            model.Status = (int)vm.Status;
            model.ReasonCode = (int)vm.ReasonCode;
            model.Reason = vm.Reason;
            model.CreatedOn = DateTime.UtcNow;
            model.UpdatedOn = DateTime.UtcNow;

            return model;
        }

        public static ProductReturnViewModel ToVM(this ProductReturn model, ProductReturnViewModel vm =null)
        {
            if(vm==null)
            {
                vm = new ProductReturnViewModel();
            }

            vm.Id = model.Id;
            vm.SelectedProductId = model.ProductId;
            vm.SelectedClientId = model.ClientId;
            vm.SelectedSaleOrderId = model.SaleOrderId.HasValue?model.SaleOrderId.Value:0;
            vm.Qty = model.Qty;
            vm.ReturnDate = model.ReturnDate;
            vm.Status =(ReturnProductStatus)model.Status;
            vm.ReasonCode = (ReturnReason)model.ReasonCode;
            vm.Reason = model.Reason;
            vm.CreatedOn = model.CreatedOn.ToString("");
            vm.CreatedByUser = string.Format("{0} {2}", model.CreatedBy.FirstName, model.CreatedBy.LastName);
            vm.UpdatedOn = model.UpdatedOn.ToString("");
            vm.UpdatedByUser = string.Format("{0} {2}", model.UpdatedBy.FirstName, model.UpdatedBy.LastName);

            return vm;
        }


        public static BasicProductReturnViewModel ToBasicVM(this ProductReturn model, BasicProductReturnViewModel vm = null)
        {
            if (vm == null)
            {
                vm = new BasicProductReturnViewModel();
            }

            vm.Id = model.Id;
            vm.SelectedProduct = model.Product.ProductName;
            vm.SelectedClient = model.Client.Name;
            vm.SelectedSaleOrder = model.SaleOrderId.HasValue ? model.SaleOrder.OrderNumber : string.Empty;
            vm.Qty = model.Qty;
            vm.ReturnDate = model.ReturnDate;
            vm.Status = EnumHelper.GetEnumDescription((ReturnProductStatus)model.Status);
            vm.ReasonCode = EnumHelper.GetEnumDescription((ReturnReason)model.ReasonCode); 
            vm.Reason = model.Reason;
            vm.CreatedOn = model.CreatedOn.ToString("");
            vm.CreatedByUser = string.Format("{0} {2}", model.CreatedBy.FirstName, model.CreatedBy.LastName);
            vm.UpdatedOn = model.UpdatedOn.ToString("");
            vm.UpdatedByUser = string.Format("{0} {2}", model.UpdatedBy.FirstName, model.UpdatedBy.LastName);

            return vm;
        }
    }
}
