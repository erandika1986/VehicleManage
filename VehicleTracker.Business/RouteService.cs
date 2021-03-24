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

        public async Task<RouteResponseViewModel> SaveRoute(RouteViewModel vm)
        {
            var response = new RouteResponseViewModel();
            try
            {
                var route = _db.Route.FirstOrDefault(t => t.Id == vm.Id);

                if(route==null)
                {
                    var model = vm.ToModel();

                    _db.Route.Add(model);

                    await _db.SaveChangesAsync();

                    response.Id = model.Id;
                    response.IsSuccess = true;
                    response.Message = "New Route has been added successfully";
                }
                else
                {
                    route.RouteCode = vm.RouteCode;
                    route.StartFrom = vm.StartFrom;
                    route.EndFrom = vm.EndFrom;
                    route.TotalDistance = vm.TotalDistance;

                    _db.Route.Update(route);

                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Route details has been updated successfully";
                }

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

        public List<RouteViewModel> GetAllRoutes()
        {
            var query = _db.Route.Where(t=>t.IsActive==true).OrderBy(t => t.RouteCode);

            var data = new List<RouteViewModel>();


            var pageData = query.ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });




            return data;
        }

        public RouteViewModel GetRouteById(long id)
        {
            var route = _db.Route.FirstOrDefault(t => t.Id == id).ToVm();

            return route;
        }

    }
}
