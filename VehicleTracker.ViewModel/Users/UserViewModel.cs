using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.Users
{
  public class UserViewModel
  {
    public UserViewModel()
    {
      Roles = new List<long>();
    }

    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string Password { get; set; }
    public string Image { get; set; }
    public string ImageName { get; set; }
    public int TimeZoneId { get; set; }
    public string Nicno { get; set; }

    public string NicFrontImage { get; set; }
    public string NicFrontImageName { get; set; }

    public string NicBackImage { get; set; }
    public string NicBackImageName { get; set; }

    public string DrivingLicenceNo { get; set; }

    public string DrivingLicenceFrontImage { get; set; }
    public string DrivingLicenceFrontImageName { get; set; }

    public string DrivingLicenceBackImage { get; set; }
    public string DrivingLicenceBackImageName { get; set; }

    public string PersonalAddress { get; set; }
    public bool? IsActive { get; set; }

    public List<long> Roles { get; set; }
    public string RoleText { get; set; }
  }
}
