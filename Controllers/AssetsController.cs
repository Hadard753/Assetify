using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assetify.Data;
using Assetify.Models;
using Microsoft.AspNetCore.Http;
using Assetify.Service;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;
using NewsAPI;
using NewsAPI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assetify.Controllers
{
    public class AssetsController : Controller
    {

        private readonly AssetifyContext _context;

        public AssetsController(AssetifyContext context)
        {
            _context = context;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            var assetifyContext = (from ass in _context.Assets select ass);

            if (TempData["searchedAssets"]!=null)
            {
                var searchedAssets = TempData["searchedAssets"] as Int32[];

                List<Asset> assets = _context.Assets.Where(a => searchedAssets.Contains(a.AssetID)).Include(a => a.Address).ToList();
                return View(assets);
            }
            assetifyContext = _context.Assets.Include(a => a.Address);

            //ViewBag.AssetsToView = await assetifyContext.ToListAsync();
            return View(await assetifyContext.ToListAsync());
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Address)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            ViewBag.articleCity = await getCityArticals(asset.Address.City);
            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            var userContext = UserContextService.GetUserContext(HttpContext);
            //Can't create if not logged in
            if ((userContext.sessionID == null))
            {
                return RedirectToAction("Login", "Users");
            }

            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressID");
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetID, AddressID,Price,EstimatedPrice,Furnished,TypeId,Condition,Size,GardenSize,BalconySize,Rooms,Floor,TotalFloor,NumOfParking,Description,EntryDate,IsElevator,IsBalcony,IsTerrace,IsStorage,IsRenovated,IsRealtyCommission,IsAircondition,IsMamad,IsPandorDoors,IsAccessible,IsKosherKitchen,IsKosherBoiler,IsOnPillars,IsBars,IsForSell,IsCommercial,IsRoomates,IsImmediate,IsNearTrainStation,IsNearLightTrainStation,IsNearBeach,IsActive,RemovedReason")] Asset asset)
        {
            var userContext = UserContextService.GetUserContext(HttpContext);

            if (ModelState.IsValid)
            {
                asset.CreatedAt = DateTime.Now;
                UserAsset user_asset = new UserAsset { UserID = int.Parse(userContext.sessionID), AssetID = asset.AssetID, ActionTime = DateTime.Now, Action = ActionType.PUBLISH, Asset = asset };
                foreach (var user in _context.Users)
                {
                    if (user.UserID == int.Parse(userContext.sessionID))
                    {
                        user_asset.User = user;
                        break;
                    }
                }

                _context.Add(user_asset);
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(asset);
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public int CreateAddress(string Street, string Building, string Full, double Latitude, double Longitude, string City)
        {
            Address address = new Address { Latitude = Latitude, Longitude = Longitude, Full = Full, City = City, Building = Building, Street = Street };
            _context.Add(address);
            _context.SaveChanges();
            return address.AddressID;
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //bool isValid = false;
            UserContext userContext = UserContextService.GetUserContext(HttpContext);

            if (id == null) return NotFound();
            if (userContext.sessionID == null) return RedirectToAction("Login", "Users", new { message = "You need to login in order to edit this asset" });
            bool isPublisher = _context.UserAsset.Any(ua => ua.UserID == int.Parse(userContext.sessionID) && ua.AssetID == id && ua.Action == ActionType.PUBLISH);

            if (userContext.isAdmin || isPublisher)
            {
                var asset = await _context.Assets.FindAsync(id);
                ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressID", asset.AddressID);
                return View(asset);
            }
            else
            {
                return RedirectToAction("Login", "Users", "You are not the publisher of that assert, nore or you an admin. please login with a different user");
            }
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetID,AddressID,Price,EstimatedPrice,Furnished,TypeId,Condition,Size,GardenSize,BalconySize,Rooms,Floor,TotalFloor,NumOfParking,Description,EntryDate,CreatedAt,IsElevator,IsBalcony,IsTerrace,IsStorage,IsRenovated,IsRealtyCommission,IsAircondition,IsMamad,IsPandorDoors,IsAccessible,IsKosherKitchen,IsKosherBoiler,IsOnPillars,IsBars,IsForSell,IsCommercial,IsRoomates,IsImmediate,IsNearTrainStation,IsNearLightTrainStation,IsNearBeach,IsActive,RemovedReason")] Asset asset)
        {
            if (id != asset.AssetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.AssetID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressID", asset.AddressID);
            return View(asset);
            
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Address)
                .FirstOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.AssetID == id);
        }

        public async Task<IActionResult> cityArticals(String cityName)
        {
            List < ArticleCity > articleCity = await getCityArticals(cityName);

            //returns articles array
            ViewBag.number_of_articals = articleCity.Count();
            ViewBag.articleCity = articleCity;
            ViewBag.cityName = cityName.ToUpperInvariant();

            return View();
        }

        public async Task<List<ArticleCity>> getCityArticals(String cityName)
        {

            List<ArticleCity> articleCity = new List<ArticleCity>();
            var newsApiClient = new NewsApiClient("f6395a1d8a3c469f9be70c0ec5075340");
            var monthStart = DateTime.Now.AddMonths(-1);
            var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q = "\"" + cityName + "\"",
                SortBy = NewsAPI.Constants.SortBys.Popularity,
                Language = NewsAPI.Constants.Languages.EN,
                From = monthStart
            });

            //Append all articles to output
            if (articlesResponse.Status == NewsAPI.Constants.Statuses.Ok)
            {
                //articlesResponse.Articles.ToList().ForEach(articleCity.add(new ArticleCity(article.Title, article.Description, article.Url, article.urlToImage)));
                //Oprtion two:
                foreach (var article in articlesResponse.Articles)
                {
                    articleCity.Add(new ArticleCity(article.Title, article.Description, article.Url, article.UrlToImage));
                }

            }

            return articleCity;
        }


        

    }

    //web api trying stuff

    public class ArticleCity
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }

        public ArticleCity(string title, string description, string url, string imageUrl)
        {
            this.title = title; this.description = description; this.url = url; this.image_url = imageUrl;
        }

    }




}
