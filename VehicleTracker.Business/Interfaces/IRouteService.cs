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
        Task<RouteResponseViewModel> AddNewRoute(RouteViewModel vm);
        Task<ResponseViewModel> UpdateRoute(RouteViewModel vm);
        Task<ResponseViewModel> DeleteRoute(long id);
        RouteViewModel GetRouteById(long id);
        PaginatedItemsViewModel<RouteViewModel> GetAllRoutes(int pageSize, int currentPage);

    }
}
