using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.ViewModel.Common
{
  public class FileContainerModel
  {
    public FileContainerModel()
    {
      Files = new List<IFormFile>();
    }
    public List<IFormFile> Files { get; set; }
    public long Id { get; set; }

  }
}
