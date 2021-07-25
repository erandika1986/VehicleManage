using Microsoft.Extensions.Configuration;
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
    protected readonly IConfiguration config;

    public BaseReportGenerator(Dictionary<string, string> reportParams, VMDBContext db, IConfiguration config)
    {
      this.reportParams = reportParams;
      this.db = db;
      this.config = config;
    }

    public virtual DownloadFileViewModel GeneratePDFReport()
    {
      throw new NotImplementedException("Method has not implemented.");
    }

    public virtual DownloadFileViewModel GenerateExcelReport()
    {
      throw new NotImplementedException("Method has not implemented.");
    }
  }
}
