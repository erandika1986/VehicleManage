using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business
{
    public class VehicleDifferentialOilChangeMilageService : IVehicleDifferentialOilChangeMilageService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleDifferentialOilChangeMilageService(IVMDBUow uow, IUserService userService)
        {
            this._uow = uow;
            this._userService = userService;
        }

        public async Task<VehicleResponseViewModel> AddNewVehicleDifferentialOilChangeMilage(VehicleDifferentialOilChangeMilageViewModel vm, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);


        var model = vm.ToModel();

        model.CreatedBy = user.Id;
        model.UpdatedBy = user.Id;


                _uow.VehicleDifferentialOilChangeMilage.Add(model);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Record has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }

        public async Task<VehicleResponseViewModel> DeleteVehicleDifferentialOilChangeMilage(long id, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vt = _uow.VehicleDifferentialOilChangeMilage.GetAll().FirstOrDefault(t => t.Id == id);
                vt.UpdatedBy = user.Id;
                vt.IsActive = false;
                vt.UpdatedOn = DateTime.UtcNow;
                _uow.VehicleDifferentialOilChangeMilage.Update(vt);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Record has been deleted.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }

        public PaginatedItemsViewModel<VehicleDifferentialOilChangeMilageViewModel> GetAllVehicleDifferentialOilChangeMilage(int vehicleId, int pageSize, int currentPage)
        {
            var query = _uow.VehicleDifferentialOilChangeMilage.GetAll().Where(t => t.VehicleId == vehicleId && t.IsActive==true).OrderByDescending(t=>t.Id);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<VehicleDifferentialOilChangeMilageViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<VehicleDifferentialOilChangeMilageViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public VehicleDifferentialOilChangeMilageViewModel GetVehicleDifferentialOilChangeMilageById(long id)
        {
            var vtvm = _uow.VehicleDifferentialOilChangeMilage.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vtvm;
        }

        public VehicleDifferentialOilChangeMilageViewModel GetLatestRecordForVehicle(long vehicleId)
        {
            var latestRecord = _uow.VehicleDifferentialOilChangeMilage.GetAll().Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
            if(latestRecord!=null)
            {
                return latestRecord.ToVm();
            }
            else
            {
                return new VehicleDifferentialOilChangeMilageViewModel();
            }
          
        }


    }
}
