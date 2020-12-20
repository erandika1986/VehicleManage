using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Interfaces
{
    public interface IProductSubCategoryService
    {
        PaginatedItemsViewModel<ProductSubCategoryViewModel> GetAllProductSubCategories(int pageSize, int currentPage);
        ProductSubCategoryViewModel GetProductSubCategoryById(long id);
        Task<ResponseViewModel> SaveProductSubCategory(ProductSubCategoryViewModel vm, string userName);
        Task<ResponseViewModel> DeleteProductSubCategory(int id, string userName);
    }
}
