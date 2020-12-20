using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Interfaces
{
    public interface IProductCategoryService
    {
        PaginatedItemsViewModel<ProductCategoryViewModel> GetAllProductCategories(int pageSize, int currentPage);
        ProductCategoryViewModel GetProductCategoryById(long id);
        Task<ResponseViewModel> SaveProductCategory(ProductCategoryViewModel vm, string userName);
        Task<ResponseViewModel> DeleteProductCategory(int id, string userName);
    }
}
