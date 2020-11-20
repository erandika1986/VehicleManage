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
    public class VehicleGreeceNipleService : IVehicleGreeceNipleService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleGreeceNipleService(IVMDBUow uow, IUserService userService)
        {
            this._uow = uow;
            this._userService = userService;
        }

        public async Task<VehicleResponseViewModel> AddNewVehicleGreeceNiple(VehicleGreeceNipleViewModel vm, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);
                VehicleGreeceNiple lastRecord = null;

                if(vm.ActualGreeceNipleReplaceDatee.HasValue)
                {
                    lastRecord = _uow.VehicleGreeceNiple.GetAll().FirstOrDefault(t => t.VehicleId == vm.VehicleId && t.IsActive==true && t.ActualGreeceNipleReplaceDate.HasValue == false);
                    lastRecord.ActualGreeceNipleReplaceDate = vm.ActualGreeceNipleReplaceDatee.Value;
                    lastRecord.UpdatedOn = DateTime.UtcNow;
                    lastRecord.UpdatedBy = user.Id;

                    _uow.VehicleGreeceNiple.Update(lastRecord);
                    await _uow.CommitAsync();
                }
                var model = new VehicleGreeceNiple()
                {
                    IsActive = true,
                    VehicleId = vm.VehicleId,
                    NextGreeceNipleReplaceDate = vm.NextGreeceNipleReplaceDate,
                    ParentId = lastRecord == null ? (long?)null : lastRecord.Id,
                    CreatedBy = user.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = user.Id,
                    UpdatedOn = DateTime.UtcNow
                };

                _uow.VehicleGreeceNiple.Add(model);
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

        public async Task<VehicleResponseViewModel> DeleteVehicleGreeceNiple(long id, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vt = _uow.VehicleGreeceNiple.GetAll().FirstOrDefault(t => t.Id == id);
                if(vt.ParentId.HasValue)
                {
                    vt.Parent.ActualGreeceNipleReplaceDate = (DateTime?)null;
                    vt.Parent.UpdatedBy = user.Id;
                    vt.Parent.UpdatedOn = DateTime.UtcNow;
                }

                vt.UpdatedBy = user.Id;
                vt.IsActive = false;
                vt.UpdatedOn = DateTime.UtcNow;
                _uow.VehicleGreeceNiple.Update(vt);
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

        public PaginatedItemsViewModel<VehicleGreeceNipleViewModel> GetAllVehicleGreeceNiple(int vehicleId, int pageSize, int currentPage)
        {
            var query = _uow.VehicleGreeceNiple.GetAll().Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<VehicleGreeceNipleViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<VehicleGreeceNipleViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public VehicleGreeceNipleViewModel GetVehicleGreeceNipleById(long id)
        {
            var vtvm = _uow.VehicleGreeceNiple.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vtvm;
        }


        public VehicleGreeceNipleViewModel GetLatestRecordForVehicle(long vehicleId)
        {
            var latestRecord = _uow.VehicleGreeceNiple.GetAll().Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
            if (latestRecord != null)
            {
                return latestRecord.ToVm();
            }
            else
            {
                return new VehicleGreeceNipleViewModel();
            }

        }
    }
}
