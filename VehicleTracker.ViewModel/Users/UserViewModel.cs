using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Users
{
  public class UserViewModel
  {
    public UserViewModel()
    {
      Roles = new List<RoleViewModel>();
    }

    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string Password { get; set; }
    public string Image { get; set; }
    public int TimeZoneId { get; set; }
    public string Nicno { get; set; }
    public string NicFrontImage { get; set; }
    public string NicBackImage { get; set; }
    public string DrivingLicenceNo { get; set; }
    public string DrivingLicenceFrontImage { get; set; }
    public string DrivingLicenceBackImage { get; set; }
    public string PersonalAddress { get; set; }
    public bool? IsActive { get; set; }

    public List<RoleViewModel> Roles { get; set; }
  }
}
