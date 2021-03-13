using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model.Enums;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Interfaces
{
    public interface IMasterDataCodeSevice
    {
        List<DropDownViewModal> GetAllCodeTypes();
        List<CodeViewModel> GetAllCodesForSelectedCodeType(Codes type);
        Task<ResponseViewModel> SaveCode(CodeViewModel vm);
        Task<ResponseViewModel> DeleteCode(CodeViewModel vm);


    }
}
