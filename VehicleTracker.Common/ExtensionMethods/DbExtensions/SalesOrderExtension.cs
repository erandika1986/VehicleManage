using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.SalesOrder;

namespace System
{
    public static class SalesOrderExtension
    {
        public static decimal CalculateSubTotal(this SalesOrder salesOrder)
        {
            return salesOrder.SalesOrderItems.Sum(x => x.Total);
        }

        public static decimal CalculateTaxAmount(this SalesOrder salesOrder)
        {
            return (salesOrder.SalesOrderItems.Sum(x => x.Total) * salesOrder.TaxRate) / 100.00m;
        }

        public static decimal CulculateTotalAmount(this SalesOrder salesOrder)
        {
            return salesOrder.SubTotal + salesOrder.TaxRate + salesOrder.ShippingCharge - salesOrder.Discount;
        }

        public static BasicSalesOrderDetailViewModel ToBasicVM(this SalesOrder model, BasicSalesOrderDetailViewModel vm=null)
        {
            if(vm==null)
            {
                vm = new BasicSalesOrderDetailViewModel();
            }

            vm.CreatedBy = string.Format("{0} {1}", model.CreatedBy.FirstName, model.CreatedBy.LastName);
            vm.CreatedOn = model.CreatedOn.ToString("MM/dd/yyyy");
            vm.OrderDate = model.OrderDate.ToString("MM/dd/yyyy");
            vm.OrderNumber = model.OrderNumber;
            vm.OwnerAddress = model.Owner != null ? model.Owner.Address : string.Empty;
            vm.OwnerName = model.Owner != null ? model.Owner.Name : string.Empty;
            vm.Route = model.Owner != null ? model.Owner.Route.Name : string.Empty;
            vm.Status = model.StatusNavigation.Name;
            vm.Id = model.Id;
            vm.Total = model.TotalAmount;
            vm.TotalQty = model.SalesOrderItems.Sum(x => x.Qty);
            vm.UpdatedBy = string.Format("{0} {1}", model.UpdatedBy.FirstName, model.UpdatedBy.LastName);
            vm.UpdatedOn = model.UpdatedOn.ToString("MM/dd/yyyy");

            return vm;
        }
    }
}
