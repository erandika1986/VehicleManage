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
    public class VehicleAirCleanerService : IVehicleAirCleanerService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleAirCleanerService(IVMDBUow uow, IUserService userService)
        {
            this._uow = uow;
            this._userService = userService;
        }

        public async Task<VehicleResponseViewModel> AddNewVehicleAirCleaner(VehicleAirCleanerViewModel vm, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                VehicleAirCleaner lastRecord = null;
                if (vm.ActualAirCleanerReplaceMilage.HasValue)
                {
                    lastRecord = _uow.VehicleAirCleaner.GetAll().FirstOrDefault(t => t.VehicleId == vm.VehicleId && t.IsActive == true && t.ActualAirCleanerReplaceMilage.HasValue == false);
                    lastRecord.ActualAirCleanerReplaceMilage = vm.ActualAirCleanerReplaceMilage.Value;
                    lastRecord.UpdatedOn = DateTime.UtcNow;
                    lastRecord.UpdatedBy = user.Id;

                    _uow.VehicleAirCleaner.Update(lastRecord);
                    await _uow.CommitAsync();
                }
                var model = new VehicleAirCleaner()
                {
                    IsActive = true,
                    VehicleId = vm.VehicleId,
                    NextAirCleanerReplaceMilage = vm.NextAirCleanerReplaceMilage,
                    ParentId = lastRecord == null ? (long?)null : lastRecord.Id,
                    CreatedBy = user.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = user.Id,
                    UpdatedOn = DateTime.UtcNow
                };

                _uow.VehicleAirCleaner.Add(model);
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

        public async Task<VehicleResponseViewModel> DeleteVehicleAirCleaner(long id, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vt = _uow.VehicleAirCleaner.GetAll().FirstOrDefault(t => t.Id == id);
                if(vt.ParentId.HasValue)
                {
                    vt.Parent.ActualAirCleanerReplaceMilage = (decimal?)null;
                    vt.Parent.UpdatedOn = DateTime.UtcNow;
                    vt.Parent.UpdatedBy = user.Id;
                }

                vt.UpdatedBy = user.Id;
                vt.IsActive = false;
                vt.UpdatedOn = DateTime.UtcNow;
                _uow.VehicleAirCleaner.Update(vt);
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

        public PaginatedItemsViewModel<VehicleAirCleanerViewModel> GetAllVehicleAirCleaner(int vehicleId, int pageSize, int currentPage)
        {
            var query = _uow.VehicleAirCleaner.GetAll().Where(t => t.VehicleId == vehicleId && t.IsActive==true).OrderByDescending(t=>t.Id);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<VehicleAirCleanerViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<VehicleAirCleanerViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public VehicleAirCleanerViewModel GetLatestRecordForVehicle(long vehicleId)
        {
            var latestRecord = _uow.VehicleAirCleaner.GetAll().Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
            if (latestRecord != null)
            {
                return latestRecord.ToVm();
            }
            else
            {
                return new VehicleAirCleanerViewModel();
            }
        }

        public VehicleAirCleanerViewModel GetVehicleAirCleanerById(long id)
        {
            var vtvm = _uow.VehicleAirCleaner.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vtvm;
        }


    }
}
