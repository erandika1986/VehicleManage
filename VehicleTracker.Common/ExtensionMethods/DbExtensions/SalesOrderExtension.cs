using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;

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
    }
}
