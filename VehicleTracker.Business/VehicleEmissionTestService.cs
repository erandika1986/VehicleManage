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
    public class VehicleEmissionTestService : IVehicleEmissionTestService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleEmissionTestService(IVMDBUow uow, IUserService userService)
        {
            this._uow = uow;
            this._userService = userService;
        }

        public async Task<VehicleResponseViewModel> AddNewVehicleEmissionTest(VehicleEmissionTestViewModel vm, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                VehicleEmissiontTest lastRecord = null;

                if (vm.ActualEmissiontTestDate.HasValue)
                {
                    lastRecord = _uow.VehicleEmissiontTest.GetAll().FirstOrDefault(t => t.VehicleId == vm.VehicleId && t.IsActive == true && t.ActualEmissiontTestDate.HasValue == false);
                    lastRecord.ActualEmissiontTestDate = vm.ActualEmissiontTestDate.Value;
                    lastRecord.UpdatedOn = DateTime.UtcNow;
                    lastRecord.UpdatedBy = user.Id;

                    _uow.VehicleEmissiontTest.Update(lastRecord);
                    await _uow.CommitAsync();
                }
                var model = new VehicleEmissiontTest()
                {
                    IsActive = true,
                    VehicleId = vm.VehicleId,
                    NextEmissiontTestDate = vm.NextEmissiontTestDate,
                    ParentId = lastRecord == null ? (long?)null : lastRecord.Id,
                    CreatedBy = user.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = user.Id,
                    UpdatedOn = DateTime.UtcNow
                };

                _uow.VehicleEmissiontTest.Add(model);
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

        public async Task<VehicleResponseViewModel> DeleteVehicleEmissionTest(long id, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vt = _uow.VehicleEmissiontTest.GetAll().FirstOrDefault(t => t.Id == id);
                if(vt.ParentId.HasValue)
                {
                    vt.Parent.ActualEmissiontTestDate = (DateTime?)null;
                    vt.UpdatedBy = user.Id;
                    vt.UpdatedOn = DateTime.UtcNow;
                }

                vt.UpdatedBy = user.Id;
                vt.IsActive = false;
                vt.UpdatedOn = DateTime.UtcNow;
                _uow.VehicleEmissiontTest.Update(vt);
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

        public PaginatedItemsViewModel<VehicleEmissionTestViewModel> GetAllVehicleEmissionTest(int vehicleId, int pageSize, int currentPage)
        {
            var query = _uow.VehicleEmissiontTest.GetAll().Where(t => t.VehicleId == vehicleId && t.IsActive == true).OrderByDescending(t => t.Id);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<VehicleEmissionTestViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<VehicleEmissionTestViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;
        }

        public VehicleEmissionTestViewModel GetLatestRecordForVehicle(long vehicleId)
        {
            var latestRecord = _uow.VehicleEmissiontTest.GetAll().Where(t => t.VehicleId == vehicleId).OrderByDescending(t => t.Id).FirstOrDefault();
            if (latestRecord != null)
            {
                return latestRecord.ToVm();
            }
            else
            {
                return new VehicleEmissionTestViewModel();
            }
        }

        public VehicleEmissionTestViewModel GetVehicleEmissionTestById(long id)
        {
            var vtvm = _uow.VehicleEmissiontTest.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vtvm;
        }


    }
}
