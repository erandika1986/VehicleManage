using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Vehicle;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common.Enums;
using VehicleTracker.Common;

namespace VehicleTracker.Business
{
    public class VehicleService : IVehicleService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleService(IVMDBUow uow, IUserService userService)
        {
            this._uow = uow;
            this._userService = userService;
        }

        public VehicleTypeMasterDataViewModel GetVehicleTypeMasterData()
        {
            var masterData = new VehicleTypeMasterDataViewModel();

            foreach (FuelType type in (FuelType[])Enum.GetValues(typeof(FuelType)))
            {
                masterData.FuelTypes.Add(new DropDownViewModal() { Id = (int)type, Name = EnumHelper.GetEnumDescription(type) });
            }

            var breakOilCodes = _uow.BreakOilCodes.GetAll().ToList();
            masterData.BreakOilTypes.Add(new DropDownViewModal() { Id = 0, Name = "None" });
            breakOilCodes.ForEach(item =>
            {
                masterData.BreakOilTypes.Add(new DropDownViewModal() { Id = item.Id, Name = item.Code });
            });

            var differentialOilCodes = _uow.DifferentialOilCodes.GetAll().ToList();
            masterData.DifferentialOilTypes.Add(new DropDownViewModal() { Id = 0, Name = "None" });
            differentialOilCodes.ForEach(item =>
            {
                masterData.DifferentialOilTypes.Add(new DropDownViewModal() { Id = item.Id, Name = item.Code });
            });

            var engineOilCoolants = _uow.EgineCoolants.GetAll().ToList();
            masterData.CoolantsTypes.Add(new DropDownViewModal() { Id = 0, Name = "None" });
            engineOilCoolants.ForEach(item =>
            {
                masterData.CoolantsTypes.Add(new DropDownViewModal() { Id = item.Id, Name = item.Code });
            });

            var enginOilCodes = _uow.EngineOilCodes.GetAll().ToList();
            masterData.EngineOilTypes.Add(new DropDownViewModal() { Id = 0, Name = "None" });
            enginOilCodes.ForEach(item =>
            {
                masterData.EngineOilTypes.Add(new DropDownViewModal() { Id = item.Id, Name = item.Code });
            });

            var gearBoxOilCodes = _uow.GearBoxOilCodes.GetAll().ToList();
            masterData.GearBoxOilTypes.Add(new DropDownViewModal() { Id = 0, Name = "None" });
            gearBoxOilCodes.ForEach(item =>
            {
                masterData.GearBoxOilTypes.Add(new DropDownViewModal() { Id = item.Id, Name = item.Code });
            });

            var powerSteeringOilCodes = _uow.PowerSteeringOilCodes.GetAll().ToList();
            masterData.PowerSteeringOilTypes.Add(new DropDownViewModal() { Id = 0, Name = "None" });
            powerSteeringOilCodes.ForEach(item =>
            {
                masterData.PowerSteeringOilTypes.Add(new DropDownViewModal() { Id = item.Id, Name = item.Code });
            });


            return masterData;
        }
        public async Task<ResponseViewModel> AddNewVehicleType(VehicleTypeViewModel vm)
        {
            var response = new ResponseViewModel();
            try
            {
                var vt = vm.ToModel();
                _uow.VehicleType.Add(vt);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle type has been added.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> DeleteVehicleType(long id)
        {
            var response = new ResponseViewModel();
            try
            {
                var vt = _uow.VehicleType.GetAll().FirstOrDefault(t => t.Id == id);
                _uow.VehicleType.Delete(vt);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Vehicle type has been deleted.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public PaginatedItemsViewModel<VehicleTypeViewModel> GetAllVehicleTypes(int pageSize, int currentPage)
        {
            var query = _uow.VehicleType.GetAll().OrderBy(t=>t.Name);

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<VehicleTypeViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1)* pageSize).Take(pageSize).OrderBy(t => t.Name).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<VehicleTypeViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;

        }
        public VehicleTypeViewModel GetVehicleTypeById(long id)
        {
            var vtvm = _uow.VehicleType.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vtvm;
        }
        public async Task<ResponseViewModel> UpdateVehicleType(VehicleTypeViewModel vm)
        {
            var response = new ResponseViewModel();
            try
            {
                var model = _uow.VehicleType.GetAll().FirstOrDefault(t => t.Id == vm.Id);

                model.Name = vm.Name;
                model.EngineOilChangeMilage = vm.EngineOilChangeMilage;
                model.FuelFilterChangeMilage = vm.FuelFilterChangeMilage;
                model.GearBoxChangeMilage = vm.GearBoxChangeMilage;
                model.DifferentialOilChangeMilage = vm.DifferentialOilChangeMilage;
                model.FuelFilterNumber = vm.FuelFilterNumber;
                model.AirCleanerMilage = vm.AirCleanerAge;
                model.GreeceNipleAge = vm.GreeceNipleAge;
                model.InsuranceAge = vm.InsuranceAge;
                model.FitnessReportAge = vm.FitnessReportAge;
                model.EmitionTestAge = vm.EmitionTestAge;
                model.RevenueLicenceAge = vm.RevenueLicenceAge;
                model.FuelType = vm.FuelType;
                model.EngineOilId = vm.EngineOilId > 0 ? vm.EngineOilId : (int?)null;
                model.GearBoxOilId = vm.GearBoxOilId > 0 ? vm.GearBoxOilId : (int?)null;
                model.DifferentialOilId = vm.DifferentialOilId > 0 ? vm.DifferentialOilId : (int?)null;
                model.BreakOilId = vm.BreakOilId > 0 ? vm.BreakOilId : (int?)null;
                model.EngineCoolantId = vm.EngineCoolantId > 0 ? vm.EngineCoolantId : (int?)null;
                model.PowerSteeringOilId = vm.PowerSteeringOilId > 0 ? vm.PowerSteeringOilId : (int?)null;


                _uow.VehicleType.Update(model);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Vehicle type has been updated.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }


        public VehicleMasterDataViewModel GetVehicleMasterData()
        {
            var masterData = new VehicleMasterDataViewModel();

            var vehicleTypes = _uow.VehicleType.GetAll().ToList();

            vehicleTypes.ForEach(item =>
            {
                masterData.VehicleTypes.Add(new DropDownViewModal() { Id = (int)item.Id, Name = item.Name });
            });

            int currentYear = DateTime.Now.Year;

            for (int i = currentYear; i >= currentYear-30; i--)
            {
                masterData.ProductionYears.Add(new DropDownViewModal() { Id = i, Name = i.ToString() });
            }

            return masterData;
        }
        public async Task<VehicleResponseViewModel> AddNewVehicle(VehicleViewModel vm, string userName)
        {
            var response = new VehicleResponseViewModel();
            try
            {
                if(IsVehicleAlreadyExists(vm.RegistrationNo).IsSuccess)
                {
                    response.IsSuccess = false;
                    response.Message = "Vehcile already registered with system.";

                    return response;
                }

                var user = _userService.GetUserByUsername(userName);

                var vt = vm.ToModel();
                vt.UpdatedBy = user.Id;
                vt.CreatedBy = user.Id;
                vt.CreatedOn = DateTime.UtcNow;
                vt.UpdatedOn = DateTime.UtcNow;

                _uow.Vehicle.Add(vt);
                await _uow.CommitAsync();

                //vm.NextAirCleanerDetails.VehicleId = vt.Id;
                //await AddNewVehicleAirCleanerRecord(vm.NextAirCleanerDetails, userName);

                //if(vm.HasDifferentialOil)
                //{
                //    vm.NextDifferentialOilChangeMilageDetails.VehicleId = vt.Id;
                //    await AddNewVehicleDifferentialOilChangeMilageRecord(vm.NextDifferentialOilChangeMilageDetails, userName);
                //}

                //if (vm.HasFitnessReport)
                //{
                //    vm.NextFitnessReportDetails.VehicleId = vt.Id;
                //    await AddNewVehicleFitnessReportRecord(vm.NextFitnessReportDetails, userName);
                //}

                //if (vm.HasGreeceNipple)
                //{
                //    vm.NextGreeceNipleDetails.VehicleId = vt.Id;
                //    await AddNewVehicleGreeceNipleRecord(vm.NextGreeceNipleDetails, userName);
                //}

                //vm.NextEmissionTestDetails.VehicleId = vt.Id;
                //await AddNewVehicleEmissionTestRecord(vm.NextEmissionTestDetails, userName);

                //vm.NextEngineOilMilageDetails.VehicleId = vt.Id;
                //await AddNewVehicleEngineOilMilageRecord(vm.NextEngineOilMilageDetails, userName);

                //vm.NextFuelFilterMilageDetails.VehicleId = vt.Id;
                //await AddNewVehicleFuelFilterMilageRecord(vm.NextFuelFilterMilageDetails, userName);

                //vm.NextGearBoxOilMilageDetails.VehicleId = vt.Id;
                //await AddNewVehicleGearBoxOilMilageRecord(vm.NextGearBoxOilMilageDetails, userName);

                //vm.NextInsurenceRenewalDetails.VehicleId = vt.Id;
                //await AddNewVehicleInsuranceRecord(vm.NextInsurenceRenewalDetails, userName);

                //vm.NextRevenueLicenceDetails.VehicleId = vt.Id;
                //await AddNewVehicleRevenueLicenceRecord(vm.NextRevenueLicenceDetails, userName);

                response.Id = vt.Id;
                response.IsSuccess = true;
                response.Message = "New Vehicle has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> DeleteVehicle(long id)
        {
            var response = new ResponseViewModel();
            try
            {
                var vehicle = _uow.Vehicle.GetAll().FirstOrDefault(t => t.Id == id);
                vehicle.IsActive = false;
                _uow.Vehicle.Update(vehicle);

                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Vehicle has been deleted.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public PaginatedItemsViewModel<VehicleViewModel> GetAllVehicles(int pageSize, int currentPage, string searchText)
        {
            var query = _uow.Vehicle.GetAll().Where(t=> t.IsActive == true).OrderBy(t=>t.RegistrationNo);

            if(!string.IsNullOrEmpty(searchText))
            {
                query = _uow.Vehicle.GetAll().Where(t=>t.RegistrationNo.Contains(searchText)).OrderBy(t => t.RegistrationNo);
            }

            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;
            var data = new List<VehicleViewModel>();

            totalRecordCount = query.Count();
            totalPages = (double)totalRecordCount / pageSize;
            totalPageCount = (int)Math.Ceiling(totalPages);

            var pageData = query.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(t => t.RegistrationNo).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });

            var response = new PaginatedItemsViewModel<VehicleViewModel>(currentPage, pageSize, totalPageCount, totalRecordCount, data);


            return response;

        }
        public VehicleViewModel GetVehicleById(long id)
        {
            var vehicle = _uow.Vehicle.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vehicle;
        }
        public async Task<ResponseViewModel> UpdateVehicle(VehicleViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);
                var vehicle = _uow.Vehicle.GetAll().FirstOrDefault(t => t.Id == vm.Id);
                vehicle.UpdatedOn = DateTime.UtcNow;
                vehicle.UpdatedBy = user.Id;
                vehicle.ProductionYear = vm.ProductionYear;
                vehicle.InitialOdometerReading = vm.InitialOdometerReading;
                vehicle.IsActive = vm.IsActive;
      

                _uow.Vehicle.Update(vehicle);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Vehicle detail has been updated.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleAirCleanerRecord(VehicleAirCleanerViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vac = vm.ToModel();
                vac.UpdatedBy = user.Id;
                vac.CreatedBy = user.Id;

                _uow.VehicleAirCleaner.Add(vac);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Air Cleaner Record has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleDifferentialOilChangeMilageRecord(VehicleDifferentialOilChangeMilageViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vac = vm.ToModel();
                vac.UpdatedBy = user.Id;
                vac.CreatedBy = user.Id;

                _uow.VehicleDifferentialOilChangeMilage.Add(vac);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Differential Oil Change Milage Record has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleEmissionTestRecord(VehicleEmissionTestViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vet = vm.ToModel();
                vet.UpdatedBy = user.Id;
                vet.CreatedBy = user.Id;

                _uow.VehicleEmissiontTest.Add(vet);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Emission Test record has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleEngineOilMilageRecord(VehicleEngineOilMilageViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var veom = vm.ToModel();
                veom.UpdatedBy = user.Id;
                veom.CreatedBy = user.Id;

                _uow.VehicleEngineOilMilage.Add(veom);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Engine Oil Milage has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleExpenseRecord(VehicleExpenseViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var veom = vm.ToModel();
                veom.UpdatedBy = user.Id;
                veom.CreatedBy = user.Id;

                _uow.VehicleExpense.Add(veom);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Expense has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleFitnessReportRecord(VehicleFitnessReportViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vfr = vm.ToModel();
                vfr.UpdatedBy = user.Id;
                vfr.CreatedBy = user.Id;

                _uow.VehicleFitnessReport.Add(vfr);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Fitness Report has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleFuelFilterMilageRecord(VehicleFuelFilterMilageViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vffm = vm.ToModel();
                vffm.UpdatedBy = user.Id;
                vffm.CreatedBy = user.Id;

                _uow.VehicleFuelFilterMilage.Add(vffm);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Fuel Filter Milage has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleGearBoxOilMilageRecord(VehicleGearBoxOilMilageViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vgbom = vm.ToModel();
                vgbom.UpdatedBy = user.Id;
                vgbom.CreatedBy = user.Id;

                _uow.VehicleGearBoxOilMilage.Add(vgbom);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Gear Box Oil Milage has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleGreeceNipleRecord(VehicleGreeceNipleViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vgn = vm.ToModel();
                vgn.UpdatedBy = user.Id;
                vgn.CreatedBy = user.Id;

                _uow.VehicleGreeceNiple.Add(vgn);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Greece Niple record has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleInsuranceRecord(VehicleInsuranceViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vi = vm.ToModel();
                vi.UpdatedBy = user.Id;
                vi.CreatedBy = user.Id;

                _uow.VehicleInsurance.Add(vi);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle nsurance record has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }
        public async Task<ResponseViewModel> AddNewVehicleRevenueLicenceRecord(VehicleRevenueLicenceViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            try
            {
                var user = _userService.GetUserByUsername(userName);

                var vi = vm.ToModel();
                vi.UpdatedBy = user.Id;
                vi.CreatedBy = user.Id;

                _uow.VehicleRevenueLicence.Add(vi);
                await _uow.CommitAsync();

                response.IsSuccess = true;
                response.Message = "New Vehicle Revenue Licence has been added.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Operation failed.Please try again.";
            }


            return response;
        }

        public ResponseViewModel IsVehicleAlreadyExists(string regNo)
        {
            var response = new ResponseViewModel();
            var regNoInLower = regNo.Replace(" ", string.Empty).ToLower().Trim();

            var vehicle = _uow.Vehicle.GetAll().FirstOrDefault(t => t.RegistrationNo.Replace(" ", "").ToLower().Trim() == regNoInLower);
            if(vehicle==null)
            {
                response.IsSuccess = false;
                response.Message = "Vehcile not exists";
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "Vehcile already registered with system.";
            }

            return response;
        }

    }
}
