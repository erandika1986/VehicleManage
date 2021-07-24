using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Data;
using VehicleTracker.ViewModel.Common;

namespace VehicleTracker.Report
{
  public class POReportGenerator: BaseReportGenerator
  {
    public POReportGenerator(Dictionary<string, string> reportParams, VMDBContext db)
      : base(reportParams, db)
    {
      
    }

    public override FileViewModel GenerateExcelReport()
    {
      return base.GenerateExcelReport();
    }

    public override FileViewModel GeneratePDFReport()
    {
      return base.GeneratePDFReport();
    }

    private string GeneratePOReportTemplatePath()
    {
      var outPutDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
      var templatePath = Path.Combine(outPutDirectory, "ExcelTemplates\\Purchase-Order-Template.xlsx");

      return templatePath;
    }
  }
}
