using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assetify.Models;
using NewsAPI;
using NewsAPI.Models;
using Assetify.Data;
using Microsoft.EntityFrameworkCore;

namespace Assetify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AssetifyContext _context;

        public HomeController(AssetifyContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Asset> NewestAssets = _context.Assets.Include(a => a.Images).Include(a => a.Address).OrderByDescending(a => a.CreatedAt).Take(3).ToList();
            ViewBag.NewestAssets = NewestAssets;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return RedirectToAction("Login", "Users");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}






