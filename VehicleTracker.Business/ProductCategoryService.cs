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
    public class ProductCategoryService: IProductCategoryService
    {

        #region Member variable

        private readonly VMDBContext _db;
        private readonly IUserService _userService;

        #endregion

        public ProductCategoryService(VMDBContext db, IUserService userService)
        {

            this._db = db;
            this._userService = userService;
        }



        public async Task<ResponseViewModel> DeleteProductCategory(int id, string userName)
        {
            var response = new ResponseViewModel();

            try
            {

                var category = _db.ProductCategory.FirstOrDefault(d => d.Id == id);

                category = new Model.ProductCategory()
                {

                    IsActive = false
                };

                _db.ProductCategory.Update(category);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Product category has been deleted.";
            }
            catch (Exception ex)
            {

                response.IsSuccess = true;
                response.Message = "Error has been occured while deleting the data. Please try again.";
            }

            return response;
        }

        public PaginatedItemsViewModel<ProductCategoryViewModel> GetAllProductCategories(int pageSize, int currentPage)
        {
            var query = _db.ProductCategory.Where(t => t.IsActive == true).OrderBy(t => t.Name);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<ProductCategoryViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(t => t.Name).ToList();

            pageData.ForEach(p =>
            {
                data.Add(new ProductCategoryViewModel()
                {
                    Id = p.Id,
                    Description =p.Description,
                    IsActive=p.IsActive.Value,
                    Name=p.Name,
                    Picture =p.Picture
                });
            });

            var response = new PaginatedItemsViewModel<ProductCategoryViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public ProductCategoryViewModel GetProductCategoryById(long id)
        {
            var response = new ProductCategoryViewModel();

            var pCategory = _db.ProductCategory.FirstOrDefault(x => x.Id == id);

            response.Description = pCategory.Description;
            response.Id = pCategory.Id;
            response.IsActive = pCategory.IsActive.Value;
            response.Name = pCategory.Name;
            response.Picture = pCategory.Picture;

            return response;
        }

        public async Task<ResponseViewModel> SaveProductCategory(ProductCategoryViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {

                var category = _db.ProductCategory.FirstOrDefault(d => d.Id == vm.Id);

                if (category == null)
                {
                    category = new Model.ProductCategory()
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        IsActive = true,
                        Picture = vm.Picture
                    };

                    _db.ProductCategory.Add(category);
                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "New Product category has been added.";
                }
                else
                {
                    category.Description = vm.Description;
                    category.Name = vm.Name;
                    category.Picture = vm.Picture;

                    _db.ProductCategory.Update(category);
                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Product category has been updated.";
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
