using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Factories;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Common;
using VehicleTracker.Data;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business
{
    public class MasterDataCodeSevice : IMasterDataCodeSevice
    {
        #region Member variable

        private readonly VMDBContext _db;
        private readonly IUserService _userService;

        #endregion

        public MasterDataCodeSevice(VMDBContext db, IUserService userService)
        {
            this._db = db;
            this._userService = userService;
        }


        public async Task<ResponseViewModel> DeleteCode(CodeViewModel vm)
        {
            return await CodeFactory.GetCodeManager(vm.SelectedCodeType, _db).Delete(vm);
        }

        public List<CodeViewModel> GetAllCodesForSelectedCodeType(Codes type)
        {
            return CodeFactory.GetCodeManager(type, _db).GetAll();
        }

        public List<DropDownViewModel> GetAllCodeTypes()
        {
            var codeTypes = new List<DropDownViewModel>();

            foreach (Codes code in (Codes[])Enum.GetValues(typeof(Codes)))
            {
                codeTypes.Add(new DropDownViewModel() { Id = (int)code, Name = EnumHelper.GetEnumDescription(code) });
            }

            return codeTypes;
        }

        public async Task<ResponseViewModel> SaveCode(CodeViewModel vm)
        {
            return await CodeFactory.GetCodeManager(vm.SelectedCodeType, _db).Save(vm);
        }
    }
}
