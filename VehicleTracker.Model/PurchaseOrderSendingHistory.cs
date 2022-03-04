using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class PurchaseOrderSendingHistory
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int VersionNo { get; set; }
        public DateTime SentOn { get; set; }
        public long SentBy { get; set; }
        public string ToAddresses { get; set; }
        public string Ccaddresses { get; set; }
        public string PoorderPath { get; set; }
        public int SendingStatus { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual User SentByNavigation { get; set; }
    }
}
