using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Inventory;

namespace VehicleTracker.Business.Interfaces
{
  public interface IInventoryService
  {
    List<InventoryBasicDetail> GetProductInvetorySummary();
    Task<ResponseViewModel> AddNewInventoryRecords(POInventoryReceievedDetail pOInventoryReceievedDetail, string userName);
    Task<ResponseViewModel> DeleteInventory(int id);
    POInventoryReceievedDetail GetInventoryDetailsForPO(int poId);
    InventoryMasterDataViewModel GetInventoryMasterData();
  }
}
