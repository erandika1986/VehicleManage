using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.SalesOrder;

namespace VehicleTracker.Business.Interfaces
{
    public interface ISaleOrderService
    {
        List<BasicSalesOrderDetailViewModel> GetMySalesOrders(SalesOrderFilter filters, User loggedInUser);
        List<BasicSalesOrderDetailViewModel> GetAllSalesOrders(SalesOrderFilter filters);
        Task<ResponseViewModel> SaveSalesOrder(SalesOrderViewModel vm, User loggedInUser);
        Task<ResponseViewModel> SaveSalesOrderStep1(SalesOrderStep1ViewModel vm, User loggedInUser);
        Task<ResponseViewModel> SaveSalesOrderStep3(SalesOrderStep3ViewModel vm, User loggedInUser);
        Task<ResponseViewModel> DeleteSalesOrder(int id, User loggedInUser);
        SalesOrderMasterDataViewModel GetSalesOrderMasterData();
        List<DropDownViewModal> GetCustomersByRouteId(int routeId);
        Task<SalesOrderNumber> GetSalesOrderNumber();
        SalesOrderViewModel GetSalesOrderById(long id);
        List<ProductAvailabilityViewModel> GetWarehouseProductAvailability(int productId,int salesOrderId);
        Task<long> CreateNewSalesOrder(User loggedInUser);
        Task<ResponseViewModel> AddProductToSalesOrder(SalesOrderProduct productDetail, User loggedInUser);
        Task<ResponseViewModel> DeleteSingleProductRoSalesOrder(SalesOrderProduct productDetail, User loggedInUser);
        Task<ResponseViewModel> DeleteAllProductFromSalesOrder(int productId, int salesOrderId);
    }
}
