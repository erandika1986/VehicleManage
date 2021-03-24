using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Route;

namespace VehicleTracker.Business
{
    public interface IRouteService
    {
        Task<RouteResponseViewModel> SaveRoute(RouteViewModel vm);
        Task<ResponseViewModel> DeleteRoute(long id);
        RouteViewModel GetRouteById(long id);
        List<RouteViewModel> GetAllRoutes();

    }
}
