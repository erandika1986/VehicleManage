using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Customer;

namespace VehicleTracker.Business
{
    public class CustomerService: ICustomerService
    {
        #region Member variable

        private readonly VMDBContext _db;
        private readonly IUserService _userService;

        #endregion

        public CustomerService(VMDBContext db, IUserService userService)
        {

            this._db = db;
            this._userService = userService;
        }

        public async Task<ResponseViewModel> SaveCustomer(CustomerViewModel vm,string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var client = _db.Clients.FirstOrDefault(x => x.Id == vm.Id);
                if(client==null)
                {
                    client = vm.ToModel();
                    client.CreatedOn = DateTime.UtcNow;
                    client.CreatedById = user.Id;
                    client.UpdatedOn = DateTime.UtcNow;
                    client.UpdatedById = user.Id;

                    _db.Clients.Add(client);
                    response.Message = "New Client has been saved.";
                }
                else
                {
                    client.Name = vm.Name;
                    client.Description = vm.Description;
                    client.ContactNo1 = vm.ContactNo1;
                    client.ContactNo2 = vm.ContactNo2;
                    client.Email = vm.Email;
                    client.Address = vm.Address;
                    client.Priority = vm.Priority;
                    client.RouteId = vm.RouteId;
                    client.Longitude = vm.Longitude;
                    client.Latitude = vm.Latitude;
                    client.UpdatedOn = DateTime.UtcNow;
                    client.UpdatedById = user.Id;

                    _db.Clients.Update(client);
                    response.Message = "Selected client details has been updated.";
                }


                await _db.SaveChangesAsync();

                response.IsSuccess = true;


            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured while adding new customer. Please try again.";
            }

            return response;
        }

        public async Task<ResponseViewModel> DeleteCustomer(int id, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var client = _db.Clients.FirstOrDefault(t => t.Id == id);
                client.IsActive = false;
                client.UpdatedOn = DateTime.UtcNow;
                client.UpdatedById = user.Id;

                _db.Clients.Update(client);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Selected client has been deleted.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the selected customer. Please try again.";
            }

            return response;

        }
       

        public List<CustomerViewModel> GetAllCustomers()
        {
            var query = _db.Clients.Where(t => t.IsActive == true).OrderBy(t => t.Name);

            //int totalRecordCount = 0;
            //double totalPages = 0;
            //int totalPageCount = 0;
            var data = new List<CustomerViewModel>();

            //totalRecordCount = query.Count();
            //totalPages = (double)totalRecordCount / pageSize;
            //totalPageCount = (int)Math.Ceiling(totalPages);

            //var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(t => t.Name).ToList();
            var pageData = query.ToList();
            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            //var response = new PaginatedItemsViewModel<CustomerViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return data;
        }

        public CustomerViewModel GetCustomerById(long id)
        {
            var client = _db.Clients.FirstOrDefault(t => t.Id == id);

            return client.ToVm();
        }

        public CustomerMasterDataViewModel GetCustomerMasterData()
        {
            var response = new CustomerMasterDataViewModel();

         
            foreach (ClientPriority value in Enum.GetValues(typeof(ClientPriority)))
            {
                response.Priorities.Add(new DropDownViewModal() { Id = (int)value, Name = EnumHelper.GetEnumDescription(value) });
            }

            
                var routes = _db.Routes
                    .Where(x => x.IsActive==true)
                    .Select(u => new DropDownViewModal() { Id = u.Id }).ToList();




            return response;
        }



        //public List<DropDownViewModal> GetAllRoutes()
        //{
        //    var routes = _db.UserRoles
        //        .Where(x => x.RoleId == (int)RoleType.Route || x.RoleId == (int)RoleType.Route)
        //        .Select(u => new DropDownViewModal() { Id = u.Route.Id, Name = string.Format("{0} {1}", u.User.FirstName, u.User.LastName) }).Distinct().ToList();

        //    return routes;
        //}


    }
}
