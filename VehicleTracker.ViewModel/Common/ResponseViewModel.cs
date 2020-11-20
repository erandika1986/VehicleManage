using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.ViewModel.Common
{
    public class ResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class VehicleResponseViewModel : ResponseViewModel
    {
        public long Id { get; set; }
    }

    public class RouteResponseViewModel:ResponseViewModel
    {
        public long Id { get; set; }
    }
}
