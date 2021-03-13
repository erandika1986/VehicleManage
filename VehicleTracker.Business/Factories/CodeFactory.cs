using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Data;
using VehicleTracker.Model.Enums;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Business.Factories
{
    public static class CodeFactory
    {
        public static MainCodeManager GetCodeManager(Codes selectedCodeType, VMDBContext context) 
        {
            switch(selectedCodeType)
            {
                case Codes.BreakOilCode:
                    {
                        return new BreakOilCodeManager(context, selectedCodeType);
                    }
                case Codes.DifferenialOilCode:
                    {
                        return new DifferentialOilCodeManager(context, selectedCodeType);
                    }
                case Codes.EnginCoolantCode:
                    {
                        return new EngineCoolantCodeManager(context, selectedCodeType);
                    }
                case Codes.EngineOilCode:
                    {
                        return new EngineOilCodeManager(context, selectedCodeType);
                    }
                case Codes.GearBoxOilCode:
                    {
                        return new GearBoxOilCodeManager(context, selectedCodeType);
                    }
                case Codes.PowerStreeringOilCode:
                    {
                        return new PowerSteeringOilCodeManager(context, selectedCodeType);
                    }
            }

            throw new NotImplementedException();
        }
    }


}
