using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Users;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleViewModel
    {
        public long Id { get; set; }
        public string RegistrationNo { get; set; }
        public int ProductionYear { get; set; }
        public long VehicelType { get; set; }
        public decimal InitialOdometerReading { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? IsActive { get; set; }


        //For table view
        public string VehicelTypeName { get; set; }

        ////For Vehicle List View
        //public string VehicleTypeName { get; set; }

        //public bool HasDifferentialOil { get; set; }
        //public VehicleDifferentialOilChangeMilageViewModel NextDifferentialOilChangeMilageDetails { get; set; }

        //public bool HasFitnessReport { get; set; }
        //public VehicleFitnessReportViewModel NextFitnessReportDetails { get; set; }

        //public bool HasGreeceNipple { get; set; }
        //public VehicleGreeceNipleViewModel NextGreeceNipleDetails { get; set; }

        //public VehicleInsuranceViewModel NextInsurenceRenewalDetails { get; set; }
        //public VehicleRevenueLicenceViewModel NextRevenueLicenceDetails { get; set; }
        //public VehicleEmissionTestViewModel NextEmissionTestDetails { get; set; }
        //public VehicleAirCleanerViewModel NextAirCleanerDetails { get; set; }
        //public VehicleEngineOilMilageViewModel NextEngineOilMilageDetails { get; set; }
        //public VehicleFuelFilterMilageViewModel NextFuelFilterMilageDetails { get; set; }
        //public VehicleGearBoxOilMilageViewModel NextGearBoxOilMilageDetails { get; set; }
    }
}
