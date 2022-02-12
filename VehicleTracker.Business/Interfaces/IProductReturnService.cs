using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.ProductReturn;

namespace VehicleTracker.Business.Interfaces
{
    public interface IProductReturnService
    {
        ProductReturnMasterDataViewModel GetProductReturnMasterData();
        PaginatedItemsViewModel<BasicProductReturnViewModel> GetAllVehicleReturnRecord(ProductReturnFilterViewModel filters, User loggedInUser);
        Task<ResponseViewModel> SaveProductReturn(ProductReturnViewModel vm, User loggedInUser);
        Task<ResponseViewModel> DeleteProductReturn(int id, User loggedInUser);
        List<DropDownViewModal> GetSalesOrderListForSelectedClient(int clientId);
        ProductReturnViewModel GetProductReturn(int id);
    }
}
