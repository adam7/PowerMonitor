using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerMonitor.Web.Models.ChartData
{
    public class BarChartDataset
    {
        public string label { get; set; }
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string highlightFill { get; set; }
        public string highlightStroke { get; set; }
        public IEnumerable<decimal> data { get; set; }
    }

    public class BarChart
    {
        public IEnumerable<string> labels { get; set; }
        public IEnumerable<BarChartDataset> datasets { get; set; }
    }
}