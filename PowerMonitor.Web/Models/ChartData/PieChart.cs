using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerMonitor.Web.Models.ChartData
{
    public class PieChartDataset
    {
        public decimal value { get; set; }
        public string color { get; set; }
        public string highlight { get; set; }
        public string label { get; set; }
    }

    public  class PieChart : List<PieChartDataset>
    {
    }
}