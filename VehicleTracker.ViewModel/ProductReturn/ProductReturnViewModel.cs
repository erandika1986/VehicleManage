using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model.Enums;

namespace VehicleTracker.ViewModel.ProductReturn
{
    public class ProductReturnViewModel
    {
        public int Id { get; set; }
        public int SelectedProductId { get; set; }
        public int SelectedClientId { get; set; }
        public long SelectedSaleOrderId { get; set; }
        public int SelectedWarehouseId { get; set; }
        public int Qty { get; set; }
        public string BatchNo { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public DateTime? DateOfExpiration { get; set; }
        public DateTime ReturnDate { get; set; }
        public ReturnProductStatus Status { get; set; }
        public ReturnReason ReasonCode { get; set; }
        public string Reason { get; set; }


        public string CreatedOn { get; set; }
        public string CreatedByUser { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedByUser { get; set; }
    }
}
