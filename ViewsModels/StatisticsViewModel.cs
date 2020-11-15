using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.ViewsModels
{
    public class ChartDataPoint
    {
        public string name { get; set; }
        public double value { get; set; }
    }

    public class Chart
    {
        public IList<ChartDataPoint> dataPoints { get; set; }
    }

    public class StatisticsViewModel
    {
        public Chart barChart { get; set; }
        public Chart pieChart { get; set; }
        public Chart searchPieChart { get; set; }
    }
}
