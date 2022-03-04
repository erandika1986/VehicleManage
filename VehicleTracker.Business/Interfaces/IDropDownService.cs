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
        List<DropDownViewModel> GetProductCategories();
        List<DropDownViewModel> GetProductSubCategories(int categoryId);
        List<DropDownViewModel> GetProducts(int subCategoryId);
        List<DropDownViewModel> GetProductsForSupplier(int subCategoryId, int supplierId);
    }
}
