using System;
using System.Collections.Generic;

namespace VehicleTracker.Model
{
    public partial class ProductReturn
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
