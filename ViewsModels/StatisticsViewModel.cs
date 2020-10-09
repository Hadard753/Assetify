using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.ViewsModels
{
    public class DataPoint
    {
        public string name { get; set; }
        public double value { get; set; }
    }
    public class StatisticsViewModel
    {
        public IList<DataPoint> dataPoints { get; set; }
    }
}
