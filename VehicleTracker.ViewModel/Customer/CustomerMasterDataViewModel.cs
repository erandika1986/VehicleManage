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
            Priorities = new List<DropDownViewModal>();
            Routes = new List<DropDownViewModal>();
        }

        public List<DropDownViewModal> Priorities { get; set; }
        public List<DropDownViewModal> Routes { get; set; }
    }
}
