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
    List<ProductSubCategoryViewModel> GetAllProductSubCategories(int categoryId);
    ProductSubCategoryViewModel GetProductSubCategoryById(int id);
    Task<ResponseViewModel> SaveProductSubCategory(ProductSubCategoryViewModel vm, string userName);
    Task<ResponseViewModel> DeleteProductSubCategory(int id, string userName);
    List<DropDownViewModal> GetProductCategories();
    Task<ResponseViewModel> UploadSubProductCategoryImage(FileContainerModel container, string userName);
    DownloadFileViewModel DownloadProductSubCategoryImage(int id);
  }
}
