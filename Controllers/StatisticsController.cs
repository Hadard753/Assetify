using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assetify.Models;
using Assetify.ViewsModels;
using Assetify.Data;
using Microsoft.EntityFrameworkCore;

namespace Assetify.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ILogger<StatisticsController> _logger;
        private readonly AssetifyContext _context;
        public StatisticsController(ILogger<StatisticsController> logger, AssetifyContext assetifyContext)
        {
            _logger = logger;
            _context = assetifyContext;
        }

        public IActionResult Index()
        {

            var TopPublishers = _context.UserAsset.Where(x => x.Action == ActionType.PUBLISH)
                .GroupBy(y => y.UserID, y => y.User.FirstName, (userId, names) => new
                {
                    Key = userId,
                    Count = names.Count(),
                    Description = names.Max()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            var dataPoints = new List<BarDataPoint>();
            TopPublishers.ForEach(x => dataPoints.Add(new BarDataPoint() { name = x.Description, value = x.Count }));
            

            var likesForDate = _context.UserAsset.Where(x => x.Action == ActionType.LIKE)
                .GroupBy(x => x.ActionTime, (actionTime, userAssets) => new
            {
                ActionTime = actionTime,
                Count = userAssets.Count()
            }).ToList();



            var lineChartDataPoints = new List<LineDataPoint>();
            likesForDate.ForEach(x => lineChartDataPoints.Add(new LineDataPoint() { date = x.ActionTime, value = x.Count }));


            var model = new StatisticsViewModel
            {
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
