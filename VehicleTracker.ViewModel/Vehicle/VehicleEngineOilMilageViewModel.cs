using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
  public class VehicleEngineOilMilageViewModel : CommonVehicleDetailViewModel
  {
    public decimal OilChangeMilage { get; set; }
    public decimal NextOilChangeMilage { get; set; }

  }
}
