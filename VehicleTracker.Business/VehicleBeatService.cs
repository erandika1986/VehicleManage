using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.VehicleBeat;

namespace VehicleTracker.Business
{
    public class VehicleBeatService : IVehicleBeatService
    {
        private readonly VMDBContext _db;
        private readonly ILogger<IVehicleBeatService> _logger;
        private readonly IUserService _userService;

        public VehicleBeatService(VMDBContext db, IUserService userService, ILogger<IVehicleBeatService> logger)
        {
            this._db = db;
            this._userService = userService;
            this._logger = logger;
        }

        public async Task<ResponseViewModel> SaveDailyVehicleBeatRecord(DailyVehicleBeatViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _userService.GetUserByUsername(userName);
                if (vm.Id == 0)
                {
                    var model = vm.ToModel();
                    model.CreatedBy = user.Id;
                    model.UpdatedBy = user.Id;

                    _db.DailyVehicleBeats.Add(model);

                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "New Vehicle Beat record has been added successfully.";
                }
                else
                {
                    var model = _db.DailyVehicleBeats.FirstOrDefault(t => t.Id == vm.Id);
                    model.Date = new DateTime(vm.DateYear, vm.DateMonth, vm.DateDay, 0, 0, 0); 
                    model.StartingMilage = vm.StartingMilage;
                    model.EndMilage = vm.EndMilage;
                    model.Status = (int)vm.Status;
                    model.VehicleId = vm.VehicleId;
                    model.RouteId = vm.RouteId;
                    model.SalesRepId = vm.SalesRepId;
                    model.DriverId = vm.DriverId;
                    model.EndMilage = vm.EndMilage;
                    model.UpdatedBy = user.Id;
                    model.UpdatedOn = DateTime.UtcNow;

                    _db.DailyVehicleBeats.Update(model);

                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Selected Vehicle Beat record has been updated successfully.";
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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

                var model = _db.DailyVehicleBeats.FirstOrDefault(t => t.Id == id);

                if(model.Status==(int)DailyBeatStatus.Planned)
                {
                    foreach (var item in model.DailyVehicleBeatOrders)
                    {
                        _db.DailyVehicleBeatOrders.Remove(item);
                    }

                    _db.DailyVehicleBeats.Remove(model);

                    await _db.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Selected Vehicle Beat record has been deleted successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "You can delete planned daily beats only.";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured.Please try again";
            }

            return response;
        }

        public async Task<ResponseViewModel> AddSalesOrdersToDailyBeat(DailyBeatSalesOrderViewModel vm, string userName)
        {
            var response = new ResponseViewModel();

            try
            {
                var user = _userService.GetUserByUsername(userName);

                var dailyVehicleBeatOrder = _db.DailyVehicleBeatOrders.FirstOrDefault(x => x.OrderId == vm.SalesOrderId && x.DailyVehicleBeatId ==vm.DailyBeatId);

                if(dailyVehicleBeatOrder==null)
                {
                    dailyVehicleBeatOrder = new DailyVehicleBeatOrder()
                    {
                        DailyVehicleBeatId=vm.DailyBeatId,
                        OrderId =vm.SalesOrderId,
                        AssignedDate=DateTime.UtcNow,
                        AssignedById = user.Id
                    };

                    _db.DailyVehicleBeatOrders.Add(dailyVehicleBeatOrder);

                    await _db.SaveChangesAsync();

                    response.IsSuccess = false;
                    response.Message = "Order has been  added to selected daily beat successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Order has been already added to selected daily beat.";
                }
               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while adding the sales order to the selected route";
            }

            return response;
        }

        public async Task<ResponseViewModel> DeleteSalesOrderFromDailyBeat(int id)
        {
            var response = new ResponseViewModel();

            try
            {

                var dailyVehicleBeatOrder = _db.DailyVehicleBeatOrders.FirstOrDefault(x => x.Id==id);

                if (dailyVehicleBeatOrder != null)
                {
                    _db.DailyVehicleBeatOrders.Remove(dailyVehicleBeatOrder);

                    await _db.SaveChangesAsync();

                    response.IsSuccess = false;
                    response.Message = "Order has been  deleted from selected daily beat successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Selected record does not exist anymore.";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while deleting the sales order from the selected daily beat";
            }

            return response;
        }

        public PaginatedItemsViewModel<DailyVehicleBeatViewModel> GetAllVehicleBeatRecord(VehicleBeatFilterViewModel filters, string userName)
        {

            var user = _userService.GetUserByUsername(userName);

            int totalRecordCount = 0;
            int totalPageCount = 0;

            var filterDate = new DateTime(filters.DateYear, filters.DateMonth, filters.DateDay, 0, 0, 0);

            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZone.TimeZoneId);

            var startDate = new DateTime(filters.DateYear, filters.DateMonth, filters.DateDay, 0, 0, 0);

            var endDate = new DateTime(filters.DateYear, filters.DateMonth, filters.DateDay, 23, 59, 59);

            //var query = _db.DailyVehicleBeats.
            //    Where(t => 
            //    TimeZoneInfo.ConvertTimeFromUtc(t.Date, cstZone) >= startDate && 
            //    TimeZoneInfo.ConvertTimeFromUtc(t.Date, cstZone) <= endDate && 
            //    t.IsActive == true)
            //    .OrderByDescending(t => t.Vehicle.RegistrationNo);

            var query = _db.DailyVehicleBeats.
                Where(t => 
                t.Date >= startDate && 
                t.Date <= endDate && 
                t.IsActive == true)
                .OrderByDescending(t => t.Vehicle.RegistrationNo);

            if (filters.SelectedDriverId>0)
            {
                query = query.Where(x=>x.DriverId==filters.SelectedDriverId).OrderByDescending(t => t.Vehicle.RegistrationNo);
            }

            if (filters.SelectedRouteId > 0)
            {
                query = query.Where(x => x.RouteId == filters.SelectedRouteId).OrderByDescending(t => t.Vehicle.RegistrationNo);
            }

            if (filters.SelectedSalesRepId > 0)
            {
                query = query.Where(x => x.SalesRepId == filters.SelectedSalesRepId).OrderByDescending(t => t.Vehicle.RegistrationNo);
            }

            if (filters.SelectedVehicleId > 0)
            {
                query = query.Where(x => x.VehicleId == filters.SelectedVehicleId).OrderByDescending(t => t.Vehicle.RegistrationNo);
            }

            var data = new List<DailyVehicleBeatViewModel>();

            totalRecordCount = query.Count();

            totalPageCount = (int)Math.Ceiling((Convert.ToDecimal(totalRecordCount)/filters.PageSize));

            var pageData = query.Skip((filters.CurrentPage-1)*filters.PageSize).Take(filters.PageSize).ToList();

            pageData.ForEach(p =>
            {
                var vm = p.ToVm();
                vm.Date = TimeZoneInfo.ConvertTimeFromUtc(vm.Date, cstZone);
                vm.CreatedOn = TimeZoneInfo.ConvertTimeFromUtc(vm.CreatedOn, cstZone);
                vm.UpdatedOn = TimeZoneInfo.ConvertTimeFromUtc(vm.UpdatedOn, cstZone);
                data.Add(vm);
            });

            var response = new PaginatedItemsViewModel<DailyVehicleBeatViewModel>(filters.CurrentPage, filters.PageSize, totalPageCount, totalRecordCount, data);

            return response;

        }

        public VehicleBeatMasterDataViewModel GetMasterData()
        {
            var response = new VehicleBeatMasterDataViewModel();
            response.Vehicles = _db.Vehicles.Where(t => t.IsActive == true).Select(v => new DropDownViewModel() { Id = v.Id, Name = v.RegistrationNo }).ToList();
            response.Routes = _db.Routes.Where(t => t.IsActive == true).Select(v => new DropDownViewModel() { Id = v.Id, Name = string.Format("{0} - ({1} to {2})", v.RouteCode, v.StartFrom, v.EndFrom) }).ToList();
            response.Drivers = _db.UserRoles.Where(t => t.IsActive == true && t.RoleId == (int)RoleType.Driver).Select(x => new DropDownViewModel() {Id = x.User.Id,Name=string.Format("{0} {1}",x.User.FirstName,x.User.LastName) }).ToList();
            response.SalesReps = _db.UserRoles.Where(t => t.IsActive == true && t.RoleId == (int)RoleType.SalesRep).Select(x => new DropDownViewModel() { Id = x.User.Id, Name = string.Format("{0} {1}", x.User.FirstName, x.User.LastName) }).ToList();
            foreach (DailyBeatStatus suit in (DailyBeatStatus[])Enum.GetValues(typeof(DailyBeatStatus)))
            {
                response.Status.Add(new DropDownViewModel() { Id = (int)suit, Name = EnumHelper.GetEnumDescription(suit) });
            }

            return response;
        }

        public DailyVehicleBeatViewModel GetVehicleBeatRecordById(long id)
        {
            var model = _db.DailyVehicleBeats.FirstOrDefault(t => t.Id == id);

            var vm = model.ToVm();

            return vm;
        }

        public async Task<ResponseViewModel> CompleteDailyBeat(int id, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var dailyBeat = _db.DailyVehicleBeats.FirstOrDefault(x => x.Id == id);

                var uncompletedOrders = dailyBeat.DailyVehicleBeatOrders.Where(x => x.DeliveredDateTime == null).ToList();
                if(uncompletedOrders.Count==0)
                {
                    dailyBeat.Status = (int)DailyBeatStatus.Completed;
                    dailyBeat.UpdatedOn = DateTime.UtcNow;
                    dailyBeat.CreatedBy = loggedInUser.Id;

                    _db.DailyVehicleBeats.Update(dailyBeat);

                    await _db.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Message = "Daily beat has been updated as completed.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Unable to complete the daily beat since all the orders are nor delivered.";
                }

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while updating the daily beat as completed. Please try again.";
            }

            return response;
        }

        public async Task<ResponseViewModel> MakeDailyBeatPartiallyCompleted(int id, User loggedInUser)
        {
            var response = new ResponseViewModel();

            try
            {
                var dailyBeat = _db.DailyVehicleBeats.FirstOrDefault(x => x.Id == id);

                var uncompletedOrders = dailyBeat.DailyVehicleBeatOrders.Where(x => x.DeliveredDateTime == null).ToList();

                foreach (var item in uncompletedOrders)
                {
                    _db.DailyVehicleBeatOrders.Remove(item);
                }

                dailyBeat.Status = (int)DailyBeatStatus.Completed;
                dailyBeat.UpdatedOn = DateTime.UtcNow;
                dailyBeat.CreatedBy = loggedInUser.Id;

                _db.DailyVehicleBeats.Update(dailyBeat);

                await _db.SaveChangesAsync();
                response.IsSuccess = true;
                response.Message = "Daily beat has been updated as partially completed.";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response.IsSuccess = false;
                response.Message = "Error has been occured while updating the daily beat as completed. Please try again.";
            }

            return response;
        }
    }
}
