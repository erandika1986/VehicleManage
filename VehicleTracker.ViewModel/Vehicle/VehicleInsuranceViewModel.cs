using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
  public class VehicleInsuranceViewModel:CommonVehicleDetailViewModel
  {

    public DateTime NextInsuranceDate { get; set; }
    public DateTime? ActualInsuranceDate { get; set; }
    public string ImageURL { get; set; }

  }
}
