using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Model;

namespace VehicleTracker.Business
{
  public class LoggedInUserService : ILoggedInUserService
  {

    #region Member variable
    private readonly VMDBContext _db;
    private readonly ILogger<ILoggedInUserService> _logger;

    #endregion

    public LoggedInUserService(VMDBContext db, ILogger<ILoggedInUserService> logger)
    {
      this._db = db;
      this._logger = logger;
    }

    public User GetLoggedInUserByUserName(string userName)
    {
      var user = _db.Users.FirstOrDefault(t => t.Username.ToLower() == userName.ToLower());

      return user;
    }
  }
}
