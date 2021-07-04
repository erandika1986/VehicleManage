using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.PurchaseOrder;

namespace VehicleTracker.Business.Interfaces
{
  public interface IPurchaseOrderService
  {
    Task<ResponseViewModel> SavePurchaseOrder(PurchaseOrderViewModel vm,string userName);
    List<PurchaseOrderSummaryViewModel> GetAllPurchseOrder();
    PurchaseOrderViewModel GetPurchaseOrderById(int id);
    Task<ResponseViewModel> DeletePurchaseOrder(int id,string userName);
    PurchaseOrderMasterData GetPurchaseOrderMasterData();
  }
}
