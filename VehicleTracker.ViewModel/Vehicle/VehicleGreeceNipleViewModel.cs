using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Vehicle
{
  public class VehicleGreeceNipleViewModel : CommonVehicleDetailViewModel
  {

    public string GreeceNipleReplaceDate { get; set; }
    public string NextGreeceNipleReplaceDate { get; set; }

    //For Reactive Form
    public int GreeceNipleReplacYear { get; set; }
    public int GreeceNipleReplacMonth { get; set; }
    public int GreeceNipleReplacDay { get; set; }

    public int NextGreeceNipleReplaceYear { get; set; }
    public int NextGreeceNipleReplaceMonth { get; set; }
    public int NextGreeceNipleReplaceDay { get; set; }

  }
}
