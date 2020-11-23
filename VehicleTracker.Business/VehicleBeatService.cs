using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model;
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

        public async Task<ResponseViewModel> SaveDailyVehicleBeatRecord(DailyVehicleBeatViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _userService.GetUserByUsername(userName);
                if(vm.Id==0)
                {
                    var model = vm.ToModel();
                    model.CreatedBy = user.Id;
                    model.UpdatedBy = user.Id;

                    _uow.DailyVehicleBeat.Add(model);

                    await _uow.CommitAsync();

                    response.IsSuccess = true;
                    response.Message = "New Vehicle Beat record has been added successfully.";
                }
                else
                {
                    var model = _uow.DailyVehicleBeat.GetAll().FirstOrDefault(t => t.Id == vm.Id);
                    model.StartingMilage = vm.StartingMilage;
                    model.EndMilage = vm.EndMilage;
                    model.Status = (int)vm.Status;
                    model.VehicleId = vm.VehicleId;
                    model.RouteId = vm.RouteId;
                    model.EndMilage = vm.EndMilage;
                    model.UpdatedBy = user.Id;
                    model.UpdatedOn = DateTime.UtcNow;

                    _uow.DailyVehicleBeat.Update(model);

                    await _uow.CommitAsync();

                    response.IsSuccess = true;
                    response.Message = "Selected Vehicle Beat record has been updated successfully.";
                }


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

        public List<DailyVehicleBeatViewModel> GetAllVehicleBeatRecord(VehicleBeatFilterViewModel filters, string userName)
        {

            var user = _userService.GetUserByUsername(userName);
            var splitedDate = filters.Date.Split('-');
            var filterDate = new DateTime(int.Parse(splitedDate[0]), int.Parse(splitedDate[1]), int.Parse(splitedDate[2]),0,0,0);
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZone);
            //TimeZoneInfo.ConvertTimeFromUtc(new DateTime(filters.Date.Year, filters.Date.Month, filters.Date.Day, 0, 0, 0), cstZone); ;DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

            var startDate =  new DateTime(int.Parse(splitedDate[0]), int.Parse(splitedDate[1]), int.Parse(splitedDate[2]), 0, 0, 0);

            var endDate = new DateTime(int.Parse(splitedDate[0]), int.Parse(splitedDate[1]), int.Parse(splitedDate[2]), 23, 59, 59);

            var query = _uow.DailyVehicleBeat.GetAll().Where(t => TimeZoneInfo.ConvertTimeFromUtc(t.Date, cstZone )>= startDate && TimeZoneInfo.ConvertTimeFromUtc(t.Date, cstZone) <= endDate && t.IsActive==true).OrderByDescending(t=>t.Vehicle.RegistrationNo);

            //int totalRecordCount = 0;
            //double totalPages = 0;
            //int totalPageCount = 0;
            var data = new List<DailyVehicleBeatViewModel>();

            //totalRecordCount = query.Count();
            //totalPages = (double)totalRecordCount / filters.PageSize;
            //totalPageCount = (int)Math.Ceiling(totalPages);

            //var pageData = query.Skip((filters.CurrentPage - 1) * filters.PageSize).Take(filters.PageSize).ToList();
            var pageData = query.ToList();

            pageData.ForEach(p =>
            {
                var vm = p.ToVm();
                vm.Date = TimeZoneInfo.ConvertTimeFromUtc(vm.Date, cstZone);
                vm.CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(vm.CreatedOn, cstZone);
                vm.UpdatedOn = TimeZoneInfo.ConvertTimeFromUtc(vm.UpdatedOn, cstZone);
                data.Add(vm);
            });

            //var response = new PaginatedItemsViewModel<DailyVehicleBeatViewModel>(filters.CurrentPage, filters.PageSize, totalPageCount, totalRecordCount, data);

            return data;

        }

        public VehicleBeatMasterDataViewModel GetMasterData()
        {
            var response = new VehicleBeatMasterDataViewModel();
            response.Vehicles = _uow.Vehicle.GetAll().Where(t => t.IsActive == true).Select(v => new DropDownViewModal() { Id = v.Id, Name = v.RegistrationNo }).ToList();
            response.Routes = _uow.Route.GetAll().Where(t => t.IsActive == true).Select(v => new DropDownViewModal() { Id = v.Id, Name = string.Format("{0} - ({1} to {2})", v.RouteCode,v.StartFrom,v.EndFrom)}).ToList();

            foreach (DailyBeatStatus suit in (DailyBeatStatus[])Enum.GetValues(typeof(DailyBeatStatus)))
            {
                response.Status.Add(new DropDownViewModal() { Id = (int)suit, Name = EnumHelper.GetEnumDescription(suit) });
            }

            return response;
        }

        public DailyVehicleBeatViewModel GetVehicleBeatRecordById(long id)
        {
            var model = _uow.DailyVehicleBeat.GetAll().FirstOrDefault(t => t.Id == id);

            var vm = model.ToVm();

            return vm;
        }

    }
}
