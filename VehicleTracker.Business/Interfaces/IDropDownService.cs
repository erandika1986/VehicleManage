using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Interfaces
{
    public interface IDropDownService
    {
        List<DropDownViewModal> GetProductCategories();
        List<DropDownViewModal> GetProductSubCategories(int categoryId);
        List<DropDownViewModal> GetProducts(int subCategoryId);
        List<DropDownViewModal> GetProductsForSupplier(int subCategoryId, int supplierId);
    }
}
