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
    public class VehicleEngineOilMilageService : IVehicleEngineOilMilageService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleEngineOilMilageService(IVMDBUow uow, IUserService userService)
        {
            this._uow = uow;
            this._userService = userService;
        }

        public async Task<VehicleResponseViewModel> AddNewVehicleEngineOilMilage(VehicleEngineOilMilageViewModel vm, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                VehicleEngineOilMilage lastRecord = null;

                if (vm.ActualOilChangeMilage.HasValue)
                {
                    lastRecord = _uow.VehicleEngineOilMilage.GetAll().FirstOrDefault(t => t.VehicleId == vm.VehicleId && t.IsActive == true && t.ActualOilChangeMilage.HasValue == false);
                    lastRecord.ActualOilChangeMilage = vm.ActualOilChangeMilage.Value;
                    lastRecord.UpdatedOn = DateTime.UtcNow;
                    lastRecord.UpdatedBy = user.Id;

                    _uow.VehicleEngineOilMilage.Update(lastRecord);
                    await _uow.CommitAsync();
                }
                var model = new VehicleEngineOilMilage()
                {
                    IsActive = true,
                    VehicleId = vm.VehicleId,
                    NextOilChangeMilage = vm.NextOilChangeMilage,
                    ParentId = lastRecord == null ? (long?)null : lastRecord.Id,
                    CreatedBy = user.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = user.Id,
                    UpdatedOn = DateTime.UtcNow
                };

                _uow.VehicleEngineOilMilage.Add(model);
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

        public async Task<VehicleResponseViewModel> DeleteVehicleEngineOilMilage(long id, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vt = _uow.VehicleEngineOilMilage.GetAll().FirstOrDefault(t => t.Id == id);
                if(vt.ParentId.HasValue)
                {
                    vt.Parent.ActualOilChangeMilage = (decimal?)null;
                    vt.Parent.UpdatedBy = user.Id;
                    vt.Parent.UpdatedOn = DateTime.UtcNow;
                }
                vt.UpdatedBy = user.Id;
                vt.IsActive = false;
                vt.UpdatedOn = DateTime.UtcNow;
                _uow.VehicleEngineOilMilage.Update(vt);
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

        public PaginatedItemsViewModel<VehicleEngineOilMilageViewModel> GetAllVehicleEngineOilMilage(int vehicleId, int pageSize, int currentPage)
        {
            var query = _uow.VehicleEngineOilMilage.GetAll().Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<VehicleEngineOilMilageViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<VehicleEngineOilMilageViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public VehicleEngineOilMilageViewModel GetLatestRecordForVehicle(long vehicleId)
        {
            var latestRecord = _uow.VehicleEngineOilMilage.GetAll().Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
            if (latestRecord != null)
            {
                return latestRecord.ToVm();
            }
            else
            {
                return new VehicleEngineOilMilageViewModel();
            }
        }

        public VehicleEngineOilMilageViewModel GetVehicleEngineOilMilageById(long id)
        {
            var vtvm = _uow.VehicleEngineOilMilage.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vtvm;
        }


    }
}
