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
            var model = new StatisticsViewModel
            {
                barChart = new Chart { dataPoints = getTopPublisher() },
                pieChart = new Chart { dataPoints = getAssetsPerCity() },
                searchPieChart = new Chart { dataPoints = getSearchesPerCity() }
            };
            return View(model);
        }

        private List<ChartDataPoint> getTopPublisher()
        {
            var TopPublishers = _context.UserAsset.Where(x => x.Action == ActionType.PUBLISH)
                .GroupBy(y => y.UserID, y => y.User.FirstName, (userId, names) => new
                {
                    Key = userId,
                    Count = names.Count(),
                    Description = names.Max()
                })
                .OrderByDescending(x => x.Count)
                .Take(7)
                .ToList();

            var dataPoints = new List<ChartDataPoint>();
            TopPublishers.ForEach(x => dataPoints.Add(new ChartDataPoint() { name = x.Description, value = x.Count }));

            return dataPoints;
        }

        private List<ChartDataPoint> getAssetsPerCity()
        {
            var AssetsPerCity = _context.Assets.Include(a => a.Address)
               .GroupBy(y => y.Address.City, (city, records) => new
               {
                   Key = city,
                   Count = records.Count(),
                   Description = city
               })
               .OrderByDescending(x => x.Count)
               .Take(7)
               .ToList();

            var dataPoints = new List<ChartDataPoint>();
            AssetsPerCity.ForEach(x => dataPoints.Add(new ChartDataPoint() { name = x.Description, value = x.Count }));

            return dataPoints;
        }
        private List<ChartDataPoint> getSearchesPerCity()
        {
            var AssetsPerCity = _context.Searches
               .GroupBy(y => y.City, (city, records) => new
               {
                   Key = city,
                   Count = records.Count(),
                   Description = city
               })
               .OrderByDescending(x => x.Count)
               .Take(7)
               .ToList();

            var dataPoints = new List<ChartDataPoint>();
            AssetsPerCity.ForEach(x => dataPoints.Add(new ChartDataPoint() { name = x.Description, value = x.Count }));

            return dataPoints;
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
