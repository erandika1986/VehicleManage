using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracker.Infrastructure.Exceptions
{
    public class VehicleTrackerDomainException : Exception
    {
        public VehicleTrackerDomainException()
        { }

        public VehicleTrackerDomainException(string message)
            : base(message)
        { }

        public VehicleTrackerDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
