using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Supplier;

namespace VehicleTracker.Business.Interfaces
{
    public interface ISupplierService
    {
        Task<ResponseViewModel> SaveSupplier(SupplierViewModel vm);
        Task<ResponseViewModel> DeleteSupplier(long id);
        SupplierViewModel GetSupplierById(long id);
        PaginatedItemsViewModel<SupplierViewModel> GetAllSuppliers(int pageSize, int currentPage);
    }
}
