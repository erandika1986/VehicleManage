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
            FuelTypes = new List<DropDownViewModel>();
            EngineOilTypes = new List<DropDownViewModel>();
            GearBoxOilTypes = new List<DropDownViewModel>();
            DifferentialOilTypes = new List<DropDownViewModel>();
            BreakOilTypes = new List<DropDownViewModel>();
            PowerSteeringOilTypes = new List<DropDownViewModel>();
            CoolantsTypes = new List<DropDownViewModel>();
            VehicleTypes = new List<DropDownViewModel>();
        }

        public List<DropDownViewModel> FuelTypes { get; set; }
        public List<DropDownViewModel> EngineOilTypes { get; set; }
        public List<DropDownViewModel> GearBoxOilTypes { get; set; }
        public List<DropDownViewModel> DifferentialOilTypes { get; set; }
        public List<DropDownViewModel> BreakOilTypes { get; set; }
        public List<DropDownViewModel> PowerSteeringOilTypes { get; set; }
        public List<DropDownViewModel> CoolantsTypes { get; set; }

        public List<DropDownViewModel> VehicleTypes { get; set; }
    }
}
