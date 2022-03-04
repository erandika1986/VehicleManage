using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Business.Interfaces;
using VehicleTracker.Data;
using VehicleTracker.Model.Enums;

namespace VehicleTracker.Business
{
    public class AppSettingService : IAppSettingService
    {
        #region Member variable
        private readonly VMDBContext _db;
        private readonly ILogger<IAppSettingService> _logger;

        #endregion

        public AppSettingService(VMDBContext db, ILogger<IAppSettingService> logger)
        {
            this._db = db;
            this._logger = logger;
        }

        public string GetAppSettingValue(AppSettingInfor appSetting)
        {
            return _db.AppSettings.FirstOrDefault(x => x.Id == (int)appSetting).Value;
        }
    }
}
