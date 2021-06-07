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


    public bool? HasDifferentialOil { get; set; }
    public bool? HasFitnessReport { get; set; }
    public bool? HasGreeceNipple { get; set; }
  }
}
