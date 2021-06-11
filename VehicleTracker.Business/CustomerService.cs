using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

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

        public async Task<ResponseViewModel> AddNewCustomer(CustomerViewModel vm,string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);
                var model = vm.ToModel();
                model.CreatedOn = DateTime.UtcNow;
                model.CreatedById = user.Id;
                model.UpdatedOn = DateTime.UtcNow;
                model.UpdatedById = user.Id;

                _db.Clients.Add(model);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "New Client has been saved.";

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
       

        public PaginatedItemsViewModel<CustomerViewModel> GetAllCustomers(int pageSize, int currentPage)
        {
            var query = _db.Clients.Where(t => t.IsActive == true).OrderBy(t => t.Name);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<CustomerViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(t => t.Name).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<CustomerViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public CustomerViewModel GetCustomerById(long id)
        {
            var client = _db.Clients.FirstOrDefault(t => t.Id == id);

            return client.ToVm();
        }

        public async Task<ResponseViewModel> UpdateCustomer(CustomerViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var client = _db.Clients.FirstOrDefault(t => t.Id == vm.Id);
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

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Selected client details has been updated.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured while updating the selected client details. Please try again.";
            }

            return response;
        }
    }
}
