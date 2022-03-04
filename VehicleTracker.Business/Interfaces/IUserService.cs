using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Users;

namespace VehicleTracker.Business
{
  public interface IUserService
  {
    void AddNewUser();
    Task<ResponseViewModel> SaveUser(UserViewModel vm);
    User GetUserByUsername(string userName);
    Task<ResponseViewModel> DeleteUser(long id);
    UserMasterDataViewModel GetUserMasterData();
    List<UserViewModel> GetAllUsers(int roleId, int status);
    Task<ResponseViewModel> UploadUserImage(FileContainerModel container, string userName);
    DownloadFileViewModel DownloadUserImage(int id, int type);
    UserViewModel GetUserById(long id);
  }

}
