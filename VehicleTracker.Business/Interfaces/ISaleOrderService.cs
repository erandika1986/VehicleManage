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
    public List<BasicSalesOrderDetailViewModel> GetMySalesOrders(SalesOrderFilter filters, User loggedInUser);
    public List<BasicSalesOrderDetailViewModel> GetAllSalesOrders(SalesOrderFilter filters);
    public Task<ResponseViewModel> SaveSalesOrder(SalesOrderViewModel vm, User loggedInUser);
    public Task<ResponseViewModel> DeleteSalesOrder(int id, User loggedInUser);
  }
}
