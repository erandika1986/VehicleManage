using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.SalesOrder
{
    public class SalesOrderViewModel
    {
        public SalesOrderViewModel()
        {
            Items = new List<SalesOrderItemViewModel>();
        }

        public long Id { get; set; }
        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }
        public int OrderDateYear { get; set; }
        public int OrderDateMonth { get; set; }
        public int OrderDateDay { get; set; }
        public int OrderDateHour { get; set; }
        public int OrderDateMin { get; set; }

        public DateTime? DeliverDate { get; set; }
        public int DeliverDateYear { get; set; }
        public int DeliverDateMonth { get; set; }
        public int DeliverDateDay { get; set; }
        public int DeliverDateHour { get; set; }
        public int DeliverDateMin { get; set; }

        public int OwnerId { get; set; }
        public long RouteId { get; set; }
        public int Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal ShippingCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }

        public List<SalesOrderItemViewModel> Items { get; set; }
    }

    public class SalesOrderStep1ViewModel
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderDateYear { get; set; }
        public int OrderDateMonth { get; set; }
        public int OrderDateDay { get; set; }
        public int OrderDateHour { get; set; }
        public int OrderDateMin { get; set; }

        public DateTime? DeliverDate { get; set; }
        public int DeliverDateYear { get; set; }
        public int DeliverDateMonth { get; set; }
        public int DeliverDateDay { get; set; }
        public int DeliverDateHour { get; set; }
        public int DeliverDateMin { get; set; }
        public int OwnerId { get; set; }
        public int Status { get; set; }
    }

    public class SalesOrderStep3ViewModel
    {
        public long Id { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal ShippingCharge { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
    }
}
