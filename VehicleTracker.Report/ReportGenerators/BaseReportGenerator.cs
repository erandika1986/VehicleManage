using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Data;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Report
{
  public class BaseReportGenerator
  {
    protected readonly Dictionary<string, string> reportParams;
    protected readonly VMDBContext db;

    public BaseReportGenerator(Dictionary<string, string> reportParams, VMDBContext db)
    {
      this.reportParams = reportParams;
      this.db = db;
    }

    public virtual FileViewModel GeneratePDFReport()
    {
      throw new NotImplementedException("Method has not implemented.");
    }

    public virtual FileViewModel GenerateExcelReport()
    {
      throw new NotImplementedException("Method has not implemented.");
    }
  }
}
