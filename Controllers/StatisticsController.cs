using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assetify.Models;
using Assetify.ViewsModels;

namespace Assetify.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(ILogger<StatisticsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // populate here the real statistics data
            var dataPoints = new[] {
                new BarDataPoint { name = "Doron", value = 26 },
                new BarDataPoint { name = "Moshe", value = 25 },
                new BarDataPoint { name = "Leon", value = 18 },
                new BarDataPoint { name = "Daniala", value = 19 },
                new BarDataPoint { name = "Bla", value = 23 },
                new BarDataPoint { name = "Vova", value = 30 },
            };

            var lineChartDataPoints = new[] {
                new LineDataPoint{date = DateTime.Now, value= 1 },
                new LineDataPoint{date = DateTime.Now.AddDays(-1), value= 1 },
                new LineDataPoint{date = DateTime.Now.AddDays(-2), value= 2 },
                new LineDataPoint{date = DateTime.Now.AddDays(-3), value= 3 },
                new LineDataPoint{date = DateTime.Now.AddDays(-4), value= 4 },
                new LineDataPoint{date = DateTime.Now.AddDays(-5), value= 5 },
                new LineDataPoint{date = DateTime.Now.AddDays(-6), value= 6 },
                new LineDataPoint{date = DateTime.Now.AddDays(-7), value= 7 },
                new LineDataPoint{date = DateTime.Now.AddDays(-8), value= 8 },
                new LineDataPoint{date = DateTime.Now.AddDays(-9), value= 9 },
            };


            var model = new StatisticsViewModel { 
                barChart = new BarChart { dataPoints = dataPoints },
                lineChart = new LineChart { dataPoints = lineChartDataPoints },
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
