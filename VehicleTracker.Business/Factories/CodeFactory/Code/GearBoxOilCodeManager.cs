using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Data;
using VehicleTracker.Model;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Factories
{
    public class GearBoxOilCodeManager : MainCodeManager
    {

        public GearBoxOilCodeManager(VMDBContext dbContext, Codes selectedCodeType) : base(dbContext, selectedCodeType)
        {

        }

        public override async Task<ResponseViewModel> Save(CodeViewModel code)
        {
            var response = new ResponseViewModel();

            try
            {
                var entity = await DbContext.GearBoxOilCodes.FindAsync(code.Id);

                if (entity == null)
                {
                    entity = new GearBoxOilCode();
                    entity.Code = code.Code;
                    DbContext.GearBoxOilCodes.Add(entity);
                }
                else
                {
                    entity.Code = code.Code;
                    DbContext.GearBoxOilCodes.Update(entity);
                }

                await DbContext.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Gear box oil code saved.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error occured while saving the gear box oil code.";
            }

            return response;
        }

        public override async Task<ResponseViewModel> Delete(CodeViewModel code)
        {
            var response = new ResponseViewModel();

            try
            {
                var entity = await DbContext.GearBoxOilCodes.FindAsync(code.Id);

                DbContext.GearBoxOilCodes.Remove(entity);

                await DbContext.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Gear box oil code removed.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Unable to delete the gear box oil code since it is already in used.";
            }

            return response;
        }

        public override List<CodeViewModel> GetAll()
        {
            var list = new List<CodeViewModel>();

            var data = DbContext.GearBoxOilCodes.ToList();

            data.ForEach(item =>
            {
                list.Add(new CodeViewModel() { Id = item.Id, Code = item.Code, SelectedCodeType = SelectedCodeType });
            });

            return list;
        }
    }
}
