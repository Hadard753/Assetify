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

namespace Assetify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public /*async  Task<IActionResult>*/IActionResult Index()
        {
            /*  ViewBag.RealEstateArticles = await getArticals();*/
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



        /*        public async Task<List<ArticleCity>> getArticals()
                {

                    List<ArticleCity> articles = new List<ArticleCity>();
                    var newsApiClient = new NewsApiClient("f6395a1d8a3c469f9be70c0ec5075340");
                    var monthStart = DateTime.Now.AddMonths(-1);
                    var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
                    {
                        Q = "\"" + "Real Estate" + "\"",
                        SortBy = NewsAPI.Constants.SortBys.Popularity,
                        Language = NewsAPI.Constants.Languages.EN,
                        From = monthStart
                    });

                    //Append all articles to output
                    if (articlesResponse.Status == NewsAPI.Constants.Statuses.Ok)
                    {

                        foreach (var article in articlesResponse.Articles)
                        {
                            articles.Add(new ArticleCity(article.Title, article.Description, article.Url, article.UrlToImage));
                        }

                    }

                    return articles;
                }
            }*/
    }
}






