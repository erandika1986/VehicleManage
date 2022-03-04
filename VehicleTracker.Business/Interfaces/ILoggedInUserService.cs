using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;

namespace VehicleTracker.Business.Interfaces
{
  public interface ILoggedInUserService
  {
    User GetLoggedInUserByUserName(string userName);
  }
}
