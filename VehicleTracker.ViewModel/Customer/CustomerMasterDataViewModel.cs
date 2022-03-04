using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.Customer
{
    public class CustomerMasterDataViewModel
    {
        public CustomerMasterDataViewModel()
        {
            Priorities = new List<DropDownViewModel>();
            Routes = new List<DropDownViewModel>();
        }

        public List<DropDownViewModel> Priorities { get; set; }
        public List<DropDownViewModel> Routes { get; set; }
    }
}
