using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class PDFSharpExtension
  {
    public static double MeasureHeight(this XGraphics gfx, string text, XFont font, int width)
    {
      var lines = text.Split('\n');

      double totalHeight = 0;

      foreach (string line in lines)
      {
        var size = gfx.MeasureString(line, font);
        double height = size.Height + (size.Height * Math.Floor(size.Width / width));

        totalHeight += height;
      }

      return totalHeight;
    }
  }
}
