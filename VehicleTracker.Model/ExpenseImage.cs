using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class ExpenseImage
    {
        public long Id { get; set; }
        public long ExpenseId { get; set; }
        public string Attachment { get; set; }
        public string AttachementName { get; set; }

        public virtual Expense Expense { get; set; }
    }
}
