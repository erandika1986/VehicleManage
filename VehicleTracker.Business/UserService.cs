using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model;
using System.Linq;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Users;

namespace VehicleTracker.Business
{
    public class UserService: IUserService
    {
        #region Member variable

        private readonly IVMDBUow _uow;

        #endregion

        #region Constructor

        public UserService(IVMDBUow uow)
        {
            this._uow = uow;
        }

        #endregion

        public void AddNewUser()
        {
            this._uow.BeginTransaction();

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

            _uow.User.Add(user);
            _uow.Commit();


            var userRole = new UserRole()
            {
                IsActive = true,
                RoleId = 1,
                StartedDate = DateTime.UtcNow,
                UserId = user.Id
            };

            _uow.UserRole.Add(userRole);
            _uow.Commit();

            this._uow.EndTransaction();
        }

        public ResponseViewModel AddNewUser(UserViewModel vm)
        {
            var response = new ResponseViewModel();

            try
            {

                this._uow.BeginTransaction();

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

                _uow.User.Add(user);
                _uow.Commit();

                foreach(var role in vm.Roles)
                {
                    var userRole = new UserRole()
                    {
                        IsActive = true,
                        RoleId = role.Id,
                        StartedDate = DateTime.UtcNow,
                        UserId = user.Id
                    };

                    _uow.UserRole.Add(userRole);
                    _uow.Commit();
                }


                this._uow.EndTransaction();

                response.IsSuccess = true;
                response.Message = "New user has been created.";
            }
            catch (Exception ex)
            {
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
                var user = this._uow.User.GetById(id);

                user.IsActive = false;
                this._uow.User.Update(user);

                this._uow.Commit();

                response.IsSuccess = true;
                response.Message = "User has been deleted";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation has been failed.Please try again.";
            }

            return response;
        }

        public User GetUserByUsername(string userName)
        {
            var user = _uow.User.GetAll().FirstOrDefault(t => t.Username.ToLower() == userName.ToLower());

            return user;
        }

        public ResponseViewModel UpdateUser(UserViewModel vm)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = this._uow.User.GetById(vm.Id);

                user.FirstName = user.FirstName;
                user.LastName = user.LastName;
                user.MobileNo = user.MobileNo;
                this._uow.User.Update(user);

                this._uow.Commit();

                response.IsSuccess = true;
                response.Message = "User has been deleted";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation has been failed.Please try again.";
            }

            return response;
        }
    }
}
