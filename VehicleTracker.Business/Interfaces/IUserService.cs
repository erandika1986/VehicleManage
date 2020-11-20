using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Users;

namespace VehicleTracker.Business
{
    public interface IUserService
    {
        void AddNewUser();
        User GetUserByUsername(string userName);
        ResponseViewModel AddNewUser(UserViewModel vm);
        ResponseViewModel UpdateUser(UserViewModel vm);
        ResponseViewModel DeleteUser(long id);
    }

}
