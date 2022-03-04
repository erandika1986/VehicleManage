using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.ProductReturn
{
    public class BasicProductReturnViewModel
    {
        public int Id { get; set; }
        public string SelectedProduct { get; set; }
        public string SelectedClient { get; set; }
        public string SelectedSaleOrder { get; set; }
        public int Qty { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
        public string ReasonCode { get; set; }
        public string Reason { get; set; }


        public string CreatedOn { get; set; }
        public string CreatedByUser { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedByUser { get; set; }
    }
}
