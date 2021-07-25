using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Data;

namespace VehicleTracker.Report
{
	public class ReportFactory
	{
    public BaseReportGenerator GetPDFGenerator(Dictionary<string, string> reportParams, VMDBContext db, IConfiguration config)
    {
      switch (reportParams["ReportType"])
      {
        case "PO":
          {
            return new POReportGenerator(reportParams, db, config);
          }
      }

      throw new Exception("Unable to find the matching PDF Generation class.");
    }
  }
}
