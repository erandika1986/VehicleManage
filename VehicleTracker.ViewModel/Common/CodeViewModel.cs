using System;
using System.Collections.Generic;
using System.Text;
using VehicleTracker.Model.Enums;

namespace VehicleTracker.ViewModel.Common
{
    public class CodeViewModel
    {
        public Codes SelectedCodeType { get; set; }

        public string SelectedCode { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
