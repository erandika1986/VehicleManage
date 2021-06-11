using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
  public class VehicleFitnessReportViewModel : CommonVehicleDetailViewModel
  {
    public string FitnessReportDate { get; set; }
    public string ValidTill { get; set; }

    public string ImageURL { get; set; }
    public string ImageName { get; set; }


    //For Reactive Form
    public int FitnessReportYear { get; set; }
    public int FitnessReportMonth { get; set; }
    public int FitnessReportDay { get; set; }

    public int ValidTillYear { get; set; }
    public int ValidTillMonth { get; set; }
    public int ValidTillDay { get; set; }
  }
}
