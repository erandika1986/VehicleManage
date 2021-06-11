using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model;
using System.Linq;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Users;
using Microsoft.Extensions.Logging;

namespace VehicleTracker.Business
{
  public class UserService : IUserService
  {
    #region Member variable

    private readonly VMDBContext _db;
    private readonly ILogger<IUserService> _logger;

    #endregion

    #region Constructor

    public UserService(VMDBContext db, ILogger<IUserService> logger)
    {
      this._db = db;
      this._logger = logger;
    }

    #endregion

    public void AddNewUser()
    {


      var user = new User()
      {
        Email = "erandika1986@gmail.com",
        FirstName = "Erandika",
        IsActive = true,
        LastName = "Sandaruwan",
        MobileNo = "0702605650",
        Password = CustomPasswordHasher.GenerateHash("system"),
        Username = "erandika1986@gmail.com",
      };

      user.UserRoles = new HashSet<UserRole>();

      var userRole = new UserRole()
      {
        IsActive = true,
        RoleId = 1,
        StartedDate = DateTime.UtcNow
      };

      user.UserRoles.Add(userRole);
      _db.SaveChanges();
    }

    public ResponseViewModel AddNewUser(UserViewModel vm)
    {
      var response = new ResponseViewModel();

      try
      {



        var user = new User()
        {
          Email = vm.Email,
          FirstName = vm.FirstName,
          IsActive = true,
          LastName = vm.LastName,
          MobileNo = vm.MobileNo,
          Password = CustomPasswordHasher.GenerateHash(vm.Password),
          Username = vm.Username,
        };

        user.UserRoles = new HashSet<UserRole>();

        foreach (var role in vm.Roles)
        {
          var userRole = new UserRole()
          {
            IsActive = true,
            RoleId = role.Id,
            StartedDate = DateTime.UtcNow
          };

          user.UserRoles.Add(userRole);

        }

        _db.SaveChanges();

        response.IsSuccess = true;
        response.Message = "New user has been created.";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Error has been occured.Please try again.";
      }

      return response;
    }

    public ResponseViewModel DeleteUser(long id)
    {
      var response = new ResponseViewModel();

      try
      {
        var user = this._db.Users.Find(id);

        user.IsActive = false;
        this._db.Users.Update(user);

        _db.SaveChanges();

        response.IsSuccess = true;
        response.Message = "User has been deleted";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Operation has been failed.Please try again.";
      }

      return response;
    }

    public User GetUserByUsername(string userName)
    {
      var user = _db.Users.FirstOrDefault(t => t.Username.ToLower() == userName.ToLower());

      return user;
    }

    public ResponseViewModel UpdateUser(UserViewModel vm)
    {
      var response = new ResponseViewModel();

      try
      {
        var user = this._db.Users.Find(vm.Id);

        user.FirstName = user.FirstName;
        user.LastName = user.LastName;
        user.MobileNo = user.MobileNo;
        this._db.Users.Update(user);

        this._db.SaveChanges();

        response.IsSuccess = true;
        response.Message = "User has been deleted";
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());
        response.IsSuccess = false;
        response.Message = "Operation has been failed.Please try again.";
      }

      return response;
    }
  }
}
