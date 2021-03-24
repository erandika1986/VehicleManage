using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.ViewModel.Common;
using VehicleTracker.ViewModel.Common.Enums;
using VehicleTracker.ViewModel.Vehicle;

namespace VehicleTracker.Business
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IVMDBUow _uow;
        private readonly IUserService _userService;

        public VehicleTypeService(IVMDBUow uow, IUserService userService)
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
        public async Task<ResponseViewModel> SaveVehicleType(VehicleTypeViewModel vm)
        {
            var response = new ResponseViewModel();
            try
            {
                var model = _uow.VehicleType.GetAll().FirstOrDefault(t => t.Id == vm.Id);
                if(model==null)
                {
                    model = vm.ToModel();
                    _uow.VehicleType.Add(model);
                    await _uow.CommitAsync();

                    response.IsSuccess = true;
                    response.Message = "New Vehicle type has been added.";
                }
                else
                {
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

            }
            catch (Exception ex)
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
        public List<VehicleTypeViewModel> GetAllVehicleTypes()
        {
            var query = _uow.VehicleType.GetAll().OrderBy(t => t.Name);

            var data = new List<VehicleTypeViewModel>();



            var pageData = query.OrderBy(t => t.Name).ToList();

            pageData.ForEach(p =>
            {
                data.Add(p.ToVm());
            });


            return data;

        }
        public VehicleTypeViewModel GetVehicleTypeById(long id)
        {
            var vtvm = _uow.VehicleType.GetAll().FirstOrDefault(t => t.Id == id).ToVm();

            return vtvm;
        }

    }
}
