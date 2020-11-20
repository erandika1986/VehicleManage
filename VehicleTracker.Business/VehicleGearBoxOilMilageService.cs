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
    public class VehicleGearBoxOilMilageService : IVehicleGearBoxOilMilageService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleGearBoxOilMilageService(IVMDBUow uow, IUserService userService)
        {
            this._uow = uow;
            this._userService = userService;
        }

        public async Task<VehicleResponseViewModel> AddNewVehicleGearBoxOilMilage(VehicleGearBoxOilMilageViewModel vm, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);
                VehicleGearBoxOilMilage lastRecord = null;

                if (vm.ActualGearBoxOilChangeMilage.HasValue)
                {
                    lastRecord = _uow.VehicleGearBoxOilMilage.GetAll().FirstOrDefault(t => t.VehicleId == vm.VehicleId && t.IsActive == true && t.ActualGearBoxOilChangeMilage.HasValue == false);
                    lastRecord.ActualGearBoxOilChangeMilage = vm.ActualGearBoxOilChangeMilage.Value;
                    lastRecord.UpdatedOn = DateTime.UtcNow;
                    lastRecord.UpdatedBy = user.Id;

                    _uow.VehicleGearBoxOilMilage.Update(lastRecord);
                    await _uow.CommitAsync();
                }
                var model = new VehicleGearBoxOilMilage()
                {
                    IsActive = true,
                    VehicleId = vm.VehicleId,
                    NextGearBoxOilChangeMilage= vm.NextGearBoxOilChangeMilage,
                    ParentId = lastRecord == null ? (long?)null : lastRecord.Id,
                    CreatedBy = user.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = user.Id,
                    UpdatedOn = DateTime.UtcNow
                };

                _uow.VehicleGearBoxOilMilage.Add(model);
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

        public async Task<VehicleResponseViewModel> DeleteVehicleGearBoxOilMilage(long id, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vt = _uow.VehicleGearBoxOilMilage.GetAll().FirstOrDefault(t => t.Id == id);
                if (vt.ParentId.HasValue)
                {
                    vt.Parent.ActualGearBoxOilChangeMilage = (decimal?)null;
                    vt.Parent.UpdatedBy = user.Id;
                    vt.Parent.UpdatedOn = DateTime.UtcNow;
                }
                vt.UpdatedBy = user.Id;
                vt.IsActive = false;
                vt.UpdatedOn = DateTime.UtcNow;
                _uow.VehicleGearBoxOilMilage.Update(vt);
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

        public PaginatedItemsViewModel<VehicleGearBoxOilMilageViewModel> GetAllVehicleGearBoxOilMilage(int vehicleId, int pageSize, int currentPage)
        {
            var query = _uow.VehicleGearBoxOilMilage.GetAll().Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<VehicleGearBoxOilMilageViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<VehicleGearBoxOilMilageViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public VehicleGearBoxOilMilageViewModel GetLatestRecordForVehicle(long vehicleId)
        {
            var latestRecord = _uow.VehicleGearBoxOilMilage.GetAll().Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
            if (latestRecord != null)
            {
                return latestRecord.ToVm();
            }
            else
            {
                return new VehicleGearBoxOilMilageViewModel();
            }
        }

        public VehicleGearBoxOilMilageViewModel GetVehicleGearBoxOilMilageById(long id)
        {
            var vtvm = _uow.VehicleGearBoxOilMilage.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vtvm;
        }


    }
}
