using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Data;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Route;

namespace VehicleTracker.Business
{
    public class RouteService : IRouteService
    {

        #region Member variable
        private readonly VMDBContext _db;
        private readonly IUserService _userService;

        #endregion

        public RouteService(VMDBContext db, IUserService userService)
        {
            this._db = db;
            this._userService = userService;
        }

        public async Task<RouteResponseViewModel> AddNewRoute(RouteViewModel vm)
        {
            var response = new RouteResponseViewModel();
            try
            {
                var model = vm.ToModel();

                _db.Route.Add(model);

                await _db.SaveChangesAsync();

                response.Id = model.Id;
                response.IsSuccess = true;
                response.Message = "New Route has been added successfully";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured while adding new route. Please try again.";
            }

            return response;
        }

        public async Task<ResponseViewModel> DeleteRoute(long id)
        {
            var response = new ResponseViewModel();
            try
            {
                var route = _db.Route.FirstOrDefault(t => t.Id == id);

                route.IsActive = false;

                _db.Route.Update(route);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Selected route has been deleted successfully.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the route. Please try again.";
            }

            return response;
        }

        public PaginatedItemsViewModel<RouteViewModel> GetAllRoutes(int pageSize, int currentPage)
        {
            var query = _db.Route.Where(t=>t.IsActive==true).OrderBy(t => t.RouteCode);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<RouteViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(t => t.RouteCode).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<RouteViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public RouteViewModel GetRouteById(long id)
        {
            var route = _db.Route.FirstOrDefault(t => t.Id == id).ToVm();

            return route;
        }
        public async Task<ResponseViewModel> UpdateRoute(RouteViewModel vm)
        {
            var response = new ResponseViewModel();
            try
            {
                var route = _db.Route.FirstOrDefault(t => t.Id == vm.Id);

                route.RouteCode = vm.RouteCode;
                route.StartFrom = vm.StartFrom;
                route.EndFrom = vm.EndFrom;
                route.TotalDistance = vm.TotalDistance;

                _db.Route.Update(route);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Route details has been updated successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured while updating the route. Please try again.";
            }

            return response;
        }
    }
}
