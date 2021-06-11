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
    public class BreakOilCodeManager: MainCodeManager
    {
        public BreakOilCodeManager(VMDBContext dbContext, Codes selectedCodeType):base(dbContext,selectedCodeType)
        {

        }


        public override async Task<ResponseViewModel> Save(CodeViewModel code)
        {
            var response = new ResponseViewModel();

            try
            {
                var entity = await DbContext.BreakOilCodes.FindAsync(code.Id);

                if (entity == null)
                {
                    entity = new BreakOilCode();
                    entity.Code = code.Code;
                    DbContext.BreakOilCodes.Add(entity);
                }
                else
                {
                    entity.Code = code.Code;
                    DbContext.BreakOilCodes.Update(entity);
                }

                await DbContext.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Break oil code saved.";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error occured while saving the break oil code.";
            }

            return response;
        }

        public override async Task<ResponseViewModel> Delete(CodeViewModel code)
        {
            var response = new ResponseViewModel();

            try
            {
                var entity = await DbContext.BreakOilCodes.FindAsync(code.Id);

                DbContext.BreakOilCodes.Remove(entity);

                await DbContext.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Break oil code removed.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Unable to delete the break oil code since it is already in used.";
            }

            return response;
        }

        public override List<CodeViewModel> GetAll()
        {
            var list = new List<CodeViewModel>();

            var data = DbContext.BreakOilCodes.ToList();

            data.ForEach(item =>
            {
                list.Add(new CodeViewModel() { Id = item.Id, Code = item.Code, SelectedCodeType = SelectedCodeType });
            });

            return list;
        }
    }
}
