using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class PurchaseOrderPayment
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public string Attachment { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }
        public bool? IsActive { get; set; }
    }
}
