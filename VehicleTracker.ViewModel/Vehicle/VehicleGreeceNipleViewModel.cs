using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleGreeceNipleViewModel
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
    public DateTime GreeceNipleReplaceDate { get; set; }
    public DateTime NextGreeceNipleReplaceDate { get; set; }

        public DateTime? CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
