using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Interfaces
{
    public interface IProductService
    {
        PaginatedItemsViewModel<ProductViewModel> GetAllProducts(int productSubCategryId,int pageSize, int currentPage);
        ProductViewModel GetProductById(long id);
        Task<ResponseViewModel> SaveProduct(ProductViewModel vm, string userName);
        Task<ResponseViewModel> DeleteProduct(int id, string userName);
    }
}
