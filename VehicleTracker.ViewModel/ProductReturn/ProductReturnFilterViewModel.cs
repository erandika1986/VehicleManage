using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.ProductReturn
{
    public class ProductReturnFilterViewModel
    {
        public int SelectedProductCategoryId { get; set; }
        public int SelectedProductSubCategoryId { get; set; }
        public int SelectedProductId { get; set; }
        public int SelectedClientId { get; set; }
        public int SelectedProductReturnStatus { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
