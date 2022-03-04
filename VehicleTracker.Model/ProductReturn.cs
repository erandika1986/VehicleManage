using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleTracker.Model
{
    public partial class ProductReturn
    {
        public ProductReturn()
        {
            ProductInventories = new HashSet<ProductInventory>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public long? SaleOrderId { get; set; }
        public int Qty { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Status { get; set; }
        public int ReasonCode { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }

        public virtual Client Client { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Product Product { get; set; }
        public virtual SalesOrder SaleOrder { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
    }
}
