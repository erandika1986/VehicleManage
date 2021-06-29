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
        List<ProductViewModel> GetAllProducts(int productSubCategryId);
        ProductViewModel GetProductById(int id);
        Task<ResponseViewModel> SaveProduct(ProductViewModel vm, string userName);
        Task<ResponseViewModel> DeleteProduct(int id, string userName);
        List<DropDownViewModal> GetProductSubCategories(int categoryId);
        Task<ResponseViewModel> UploadProductImage(FileContainerModel container, string userName);
        DownloadFileViewModel DownloadProductImage(int id);
  }
}
