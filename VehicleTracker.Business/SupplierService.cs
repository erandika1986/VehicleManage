using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Supplier;

namespace VehicleTracker.Business
{
    public class SupplierService : ISupplierService
    {
        #region Member variable

        private readonly VMDBContext _db;
        private readonly IUserService _userService;

        #endregion

        public SupplierService(VMDBContext db, IUserService userService)
        {
            this._db = db;
            this._userService = userService;
        }


        public async Task<ResponseViewModel> SaveSupplier(SupplierViewModel vm)
        {
            var response = new ResponseViewModel();
            try
            {
                var supplier = _db.Suppliers.FirstOrDefault(x => x.Id == vm.Id);

                if(supplier==null)
                {
                     supplier = vm.ToModel();

                    _db.Suppliers.Add(supplier);
                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Supplier has been created.";
                }
                else
                {
                    supplier.Name = vm.Name;
                    supplier.Description = vm.Description;
                    supplier.Address = vm.Address;
                    supplier.Phone1 = vm.Phone1;
                    supplier.Phone2 = vm.Phone2;
                    supplier.Email1 = vm.Email1;
                    supplier.Email2 = vm.Email2;

                    _db.Suppliers.Update(supplier);
                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Supplier has been updated.";
                }

            }
            catch(Exception ex)
            {
                
                response.IsSuccess = false;
                response.Message = "Error has been occured.";
            }

            return response;
        }

        public async Task<ResponseViewModel> DeleteSupplier(long id)
        {
            var response = new ResponseViewModel();

            try
            {
                var supplier = _db.Suppliers.FirstOrDefault(x => x.Id == id);

                supplier.IsActive = false;

                _db.Suppliers.Update(supplier);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Supplier has been deleted.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured.Please try again.";
            }

            return response;

        }

        public PaginatedItemsViewModel<SupplierViewModel> GetAllSuppliers(int pageSize, int currentPage)
        {
            var query = _db.Suppliers.OrderBy(t => t.Name);



            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<SupplierViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(t => t.Name).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<SupplierViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public SupplierViewModel GetSupplierById(long id)
        {
            var supplier = _db.Suppliers.FirstOrDefault(x => x.Id == id);

            return supplier.ToVm();
        }


    }
}
