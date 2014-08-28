using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerMonitor.Web.Models.ChartData
{
    public class LineChartDataset
    {
        public string label { get; set; }
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string pointColor { get; set; }
        public string pointStrokeColor { get; set; }
        public string pointHighlightFill { get; set; }
        public string pointHighlightStroke { get; set; }
        public IEnumerable<decimal> data { get; set; }
    }
    
    public class LineChart
    {
        public IEnumerable<string> labels { get; set; }
        public IEnumerable<LineChartDataset> datasets { get; set; }
    }
}