using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.ViewModel;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business
{
    public class ProductService: IProductService
    {
        #region Member variable

        private readonly VMDBContext _db;
        private readonly IUserService _userService;

        #endregion

        public ProductService()
        {

        }

        public async Task<ResponseViewModel> DeleteProduct(int id, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _userService.GetUserByUsername(userName);

                var product = _db.Product.FirstOrDefault(x => x.Id == id);
                product.IsActive = false;
                product.UpdatedOn = DateTime.UtcNow;
                product.UpdatedById = user.Id;

                _db.Product.Update(product);

                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Product has deleted successfully.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Product deletion has been failed. Please try again";
            }

            return response;

            
        }

        public PaginatedItemsViewModel<ProductViewModel> GetAllProducts(int productSubCategryId, int pageSize, int currentPage)
        {
            var query = _db.Product.OrderBy(t=>t.ProductName);

            if(productSubCategryId > 0)
            {
                query = query.Where(t => t.SubProductCategoryId == productSubCategryId).OrderBy(t=>t.ProductName);
            }


            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<ProductViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(t => t.ProductName).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<ProductViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public ProductViewModel GetProductById(long id)
        {


            var product = _db.Product.FirstOrDefault(x => x.Id == id);


            return product.ToVm();
        }

        public async Task<ResponseViewModel> SaveProduct(ProductViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _userService.GetUserByUsername(userName);

                var product = _db.Product.FirstOrDefault(x => x.Id == vm.Id);

                if(product==null)
                {
                    product = vm.ToModel();
                    product.CreatedOn = DateTime.UtcNow;
                    product.CreatedById = user.Id;
                    product.UpdatedOn = DateTime.UtcNow;
                    product.UpdatedById = user.Id;

                    _db.Product.Add(product);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    product.ProductName = vm.Name;
                    product.IsActive = vm.IsActive;
                    product.Picture = vm.Picture;
                    product.ProductCode = vm.ProductCode;
                    product.SubProductCategoryId = vm.ProductSubCategoryId;
                    product.SubProductCategoryId = vm.ProductSubCategoryId;
                    product.SupplierId = vm.SupplierId;
                    product.UnitPrice = vm.UnitPrice;
                    product.UpdatedOn = DateTime.UtcNow;
                    product.UpdatedById = user.Id;

                    _db.Product.Update(product);

                    await _db.SaveChangesAsync();
                }


                response.IsSuccess = true;
                response.Message = "Product has deleted successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Product deletion has been failed. Please try again";
            }

            return response;
        }
    }
}
