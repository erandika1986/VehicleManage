using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Model.Enums;

namespace VehicleTracker.Business.Interfaces
{
    public interface IAppSettingService
    {
        string GetAppSettingValue(AppSettingInfor appSetting);
    }
}
