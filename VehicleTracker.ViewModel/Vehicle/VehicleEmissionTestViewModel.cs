using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
  public class VehicleEmissionTestViewModel : CommonVehicleDetailViewModel
  {


    public string EmissiontTestDate { get; set; }
    public string ValidTill { get; set; }

    public string ImageURL { get; set; }
    public string ImageName { get; set; }


    //For Reactive Form
    public int EmissionTestYear { get; set; }
    public int EmissionTestMonth { get; set; }
    public int EmissionTestDay { get; set; }

    public int ValidTillYear { get; set; }
    public int ValidTillMonth { get; set; }
    public int ValidTillDay { get; set; }


  }
}
