using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
  public class VehicleRevenueLicenceViewModel : CommonVehicleDetailViewModel
  {
    public string ValidTill { get; set; }
    public string RevenueLicenceDate { get; set; }

    public string ImageURL { get; set; }
    public string ImageName { get; set; }


    //For Reactive Form
    public int RevenueLicenceYear { get; set; }
    public int RevenueLicenceMonth { get; set; }
    public int RevenueLicenceDay { get; set; }

    public int ValidTillYear { get; set; }
    public int ValidTillMonth { get; set; }
    public int ValidTillDay { get; set; }
  }
}
