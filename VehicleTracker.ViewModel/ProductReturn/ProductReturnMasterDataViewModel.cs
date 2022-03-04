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
            ProductCategories = new List<DropDownViewModel>();
            ProductReturnStatus = new List<DropDownViewModel>();
            ProductReturnReasonCodes = new List<DropDownViewModel>();
        }

        public List<DropDownViewModel> ProductCategories { get; set; }
        public List<DropDownViewModel> ProductReturnStatus { get; set; }
        public List<DropDownViewModel> ProductReturnReasonCodes { get; set; }
    }
}
