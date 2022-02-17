using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.ProductReturn
{
    public class ProductReturnMasterDataViewModel
    {
        public ProductReturnMasterDataViewModel()
        {
            ProductCategories = new List<DropDownViewModal>();
            ProductReturnStatus = new List<DropDownViewModal>();
            ProductReturnReasonCodes = new List<DropDownViewModal>();
        }

        public List<DropDownViewModal> ProductCategories { get; set; }
        public List<DropDownViewModal> ProductReturnStatus { get; set; }
        public List<DropDownViewModal> ProductReturnReasonCodes { get; set; }
    }
}
