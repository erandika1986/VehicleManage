﻿using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class OrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }

        public virtual Order Order { get; set; }
    }
}
