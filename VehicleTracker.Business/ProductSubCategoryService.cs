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
    public class ProductSubCategoryService : IProductSubCategoryService
    {
        #region Member variable

        private readonly VMDBContext _db;
        private readonly IUserService _userService;

        #endregion


        public ProductSubCategoryService(VMDBContext db, IUserService userService)
        {
            this._db = db;
            this._userService = userService;
        }

        public async Task<ResponseViewModel> DeleteProductSubCategory(int id, string userName)
        {
            var response = new ResponseViewModel();

            try
            {

                var category = _db.ProductSubCategories.FirstOrDefault(d => d.Id == id);
                category.IsActive = false;

                _db.ProductSubCategories.Update(category);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Product sub category has been deleted.";
            }
            catch (Exception ex)
            {

                response.IsSuccess = true;
                response.Message = "Error has been occured while deleting the data. Please try again.";
            }

            return response;
        }

        public PaginatedItemsViewModel<ProductSubCategoryViewModel> GetAllProductSubCategories(int pageSize, int currentPage)
        {
            var query = _db.ProductSubCategories.Where(t => t.IsActive == true).OrderBy(t => t.Name);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<ProductSubCategoryViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(t => t.Name).ToList();

            pageData.ForEach(p =>
            {
                data.Add(new ProductSubCategoryViewModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    IsActive = p.IsActive.Value,
                    Name = p.Name,
                    Picture = p.Picture,
                    ProductCategoryId = p.ProductCategoryId
                });
            });

            var response = new PaginatedItemsViewModel<ProductSubCategoryViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public ProductSubCategoryViewModel GetProductSubCategoryById(long id)
        {
            var response = new ProductSubCategoryViewModel();

            var pCategory = _db.ProductSubCategories.FirstOrDefault(x => x.Id == id);

            response.Description = pCategory.Description;
            response.Id = pCategory.Id;
            response.IsActive = pCategory.IsActive.Value;
            response.Name = pCategory.Name;
            response.Picture = pCategory.Picture;
            response.ProductCategoryId = pCategory.ProductCategoryId;

            return response;
        }

        public async Task<ResponseViewModel> SaveProductSubCategory(ProductSubCategoryViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {

                var category = _db.ProductSubCategories.FirstOrDefault(d => d.Id == vm.Id);

                if (category == null)
                {
                    category = new Model.ProductSubCategory()
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        IsActive = true,
                        Picture = vm.Picture,
                        ProductCategoryId =vm.ProductCategoryId
                    };

                    _db.ProductSubCategories.Add(category);
                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "New Product sub category has been added.";
                }
                else
                {
                    category.Description = vm.Description;
                    category.Name = vm.Name;
                    category.Picture = vm.Picture;
                    category.ProductCategoryId = vm.ProductCategoryId;

                    _db.ProductSubCategories.Update(category);
                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Product sub category has been updated.";
                }
            }
            catch (Exception ex)
            {

                response.IsSuccess = true;
                response.Message = "Error has been occured while saving the data. Please try again.";
            }

            return response;
        }
    }
}
