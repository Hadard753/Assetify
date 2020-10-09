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
            var bla = new[] {
                new DataPoint { name = "Doron", value = 26 },
                new DataPoint { name = "Moshe", value = 25 },
                new DataPoint { name = "Leon", value = 18 },
                new DataPoint { name = "Daniala", value = 19 },
                new DataPoint { name = "Bla", value = 23 },
                new DataPoint { name = "Vova", value = 30 },
            };
            var model = new StatisticsViewModel { dataPoints = bla };
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
