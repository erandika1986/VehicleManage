using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
  public class VehicleInsuranceViewModel:CommonVehicleDetailViewModel
  {
    public string InsuranceDate { get; set; }
    public string ValidTill { get; set; }

    public string ImageURL { get; set; }
    public string ImageName { get; set; }


    //For Reactive Form
    public int InsuranceYear { get; set; }
    public int InsuranceMonth { get; set; }
    public int InsuranceDay { get; set; }

    public int ValidTillYear { get; set; }
    public int ValidTillMonth { get; set; }
    public int ValidTillDay { get; set; }



  }
}
