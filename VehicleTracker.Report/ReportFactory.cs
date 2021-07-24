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
    public BaseReportGenerator GetPDFGenerator(Dictionary<string, string> reportParams, VMDBContext db)
    {
      switch (reportParams.FirstOrDefault().Value)
      {
        case "PO":
          {
            return new POReportGenerator(reportParams, db);
          }
      }

      throw new Exception("Unable to find the matching PDF Generation class.");
    }
  }
}
