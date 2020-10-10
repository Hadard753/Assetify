using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.ViewsModels
{
    public class BarDataPoint
    {
        public string name { get; set; }
        public double value { get; set; }
    }

    public class BarChart
    {
        public IList<BarDataPoint> dataPoints { get; set; }
    }

    public class LineDataPoint
    {
        public DateTime date { get; set; }
        public double value { get; set; }
    }

    public class LineChart
    {
        public IList<LineDataPoint> dataPoints { get; set; }
    }


    public class StatisticsViewModel
    {
        public BarChart barChart { get; set; }
        public LineChart lineChart { get; set; }
    }
}
