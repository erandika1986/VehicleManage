using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.ViewModel.Vehicle
{
    public class VehicleTypeMasterDataViewModel
    {
        public VehicleTypeMasterDataViewModel()
        {
            FuelTypes = new List<DropDownViewModal>();
            EngineOilTypes = new List<DropDownViewModal>();
            GearBoxOilTypes = new List<DropDownViewModal>();
            DifferentialOilTypes = new List<DropDownViewModal>();
            BreakOilTypes = new List<DropDownViewModal>();
            PowerSteeringOilTypes = new List<DropDownViewModal>();
            CoolantsTypes = new List<DropDownViewModal>();
            VehicleTypes = new List<DropDownViewModal>();
        }

        public List<DropDownViewModal> FuelTypes { get; set; }
        public List<DropDownViewModal> EngineOilTypes { get; set; }
        public List<DropDownViewModal> GearBoxOilTypes { get; set; }
        public List<DropDownViewModal> DifferentialOilTypes { get; set; }
        public List<DropDownViewModal> BreakOilTypes { get; set; }
        public List<DropDownViewModal> PowerSteeringOilTypes { get; set; }
        public List<DropDownViewModal> CoolantsTypes { get; set; }

        public List<DropDownViewModal> VehicleTypes { get; set; }
    }
}
