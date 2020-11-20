using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.VehicleBeat;

namespace VehicleTracker.Business
{
    public class VehicleBeatService : IVehicleBeatService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleBeatService(IVMDBUow uow, IUserService userService)
        {
            this._uow = uow;
            this._userService = userService;
        }

        public async Task<ResponseViewModel> AddNewVehicleBeatRecord(DailyVehicleBeatViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _userService.GetUserByUsername(userName);

                var model = vm.ToModel();
                model.CreatedBy = user.Id;
                model.UpdatedBy = user.Id;

                _uow.DailyVehicleBeat.Add(model);

                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Beat record has been added successfully.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured.Please try again";
            }

            return response;
        }

        public async Task<ResponseViewModel> DeleteSelectedBeatRecord(long id, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _userService.GetUserByUsername(userName);

                var model = _uow.DailyVehicleBeat.GetAll().FirstOrDefault(t => t.Id == id);
                model.IsActive = false;
                model.UpdatedBy = user.Id;
                model.UpdatedOn = DateTime.UtcNow;

                _uow.DailyVehicleBeat.Update(model);

                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Selected Vehicle Beat record has been deleted successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured.Please try again";
            }

            return response;
        }

        public PaginatedItemsViewModel<DailyVehicleBeatViewModel> GetAllVehicleBeatRecord(VehicleBeatFilterViewModel filters)
        {
            var query = _uow.DailyVehicleBeat.GetAll().Where(t => t.Date >= filters.FromDate && t.Date <= filters.ToDate && t.IsActive==true).OrderByDescending(t=>t.Date);

            if(filters.SelectedVehicleId>0)
            {
                query = query.Where(t=>t.VehicleId==filters.SelectedVehicleId).OrderByDescending(t => t.Date);
            }

            if(filters.SelectedRouteId>0)
            {
                query = query.Where(t => t.VehicleId == filters.SelectedRouteId).OrderByDescending(t => t.Date);
            }

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<DailyVehicleBeatViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / filters.PageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((filters.CurrentPage - 1) * filters.PageSize).Take(filters.PageSize).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<DailyVehicleBeatViewModel>(filters.CurrentPage, filters.PageSize, totalPageCount, totalRecordCount, data);

            return response;

        }

        public VehicleBeatMasterDataViewModel GetMasterData()
        {
            var response = new VehicleBeatMasterDataViewModel();
            response.Vehicles = _uow.Vehicle.GetAll().Where(t => t.IsActive == true).Select(v => new DropDownViewModal() { Id = v.Id, Name = v.RegistrationNo }).ToList();
            response.Routes = _uow.Route.GetAll().Where(t => t.IsActive == true).Select(v => new DropDownViewModal() { Id = v.Id, Name = string.Format("{0} - ({1} to {2})", v.RouteCode,v.StartFrom,v.EndFrom)}).ToList();

            return response;
        }

        public DailyVehicleBeatViewModel GetVehicleBeatRecordById(long id)
        {
            var model = _uow.DailyVehicleBeat.GetAll().FirstOrDefault(t => t.Id == id);

            var vm = model.ToVm();

            return vm;
        }

        public async Task<ResponseViewModel> UpdateNewVehicleBeatRecord(DailyVehicleBeatViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _userService.GetUserByUsername(userName);

                var model = _uow.DailyVehicleBeat.GetAll().FirstOrDefault(t => t.Id == vm.Id);
                model.VehicleId = vm.VehicleId;
                model.RouteId = vm.RouteId;
                model.EndMilage = vm.EndMilage;
                model.UpdatedBy = user.Id;
                model.UpdatedOn = DateTime.UtcNow;

                _uow.DailyVehicleBeat.Update(model);

                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Selected Vehicle Beat record has been deleted successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error has been occured.Please try again";
            }

            return response;
        }
    }
}
