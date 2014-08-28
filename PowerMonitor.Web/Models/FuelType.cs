using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PowerMonitor.Web.Models
{
    public class FuelType
    {
        public FuelType(string name, string code, Color color)
        {
            Name = name;
            Code = code;
            PointColor = string.Format("rgba({0}, {1}, {2},  1)", color.R, color.G, color.B);
            LineColor = string.Format("rgba({0}, {1}, {2},  0.6)", color.R, color.G, color.B);
            FillColor = string.Format("rgba({0}, {1}, {2},  0.1)", color.R, color.G, color.B);
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string PointColor { get; private set; }
        public string LineColor { get; private set; }
        public string FillColor { get; private set; }
    }
}