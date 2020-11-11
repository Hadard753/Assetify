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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assetify.Controllers
{
    public class SearchesController : Controller
    {
        private readonly AssetifyContext _context;

        public SearchesController(AssetifyContext context)
        {
            _context = context;
        }

        // GET: Searches/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Searches/Search
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("SearchID,UserID,IsCommercial,City,Street,Neighborhoods,IsForSell,MinPrice,MaxPrice" +
            ",MinSize,MaxSize,MinGardenSize,MaxGardenSize,MinRooms,MaxRooms,MinFloor,MaxFloor,MinTotalFloor,MaxTotalFloor,TypeIdIn,MinEntryDate" +
            ",FurnishedIn,Orientations,IsElevator,IsBalcony,IsTerrace,IsStorage,IsRenovated,IsRealtyCommission,IsAircondition,IsMamad,IsPandorDoors" +
            ",IsAccessible,IsKosherKitchen,IsKosherBoiler,IsOnPillars,IsBars,IsRoomates,IsImmediate,IsNearTrainStation,IsNearLightTrainStation,IsNearBeach")] Search search)
        {
            var userContext = UserContextService.GetUserContext(HttpContext);
            var searchedAssets = (from ass in _context.Assets join add in _context.Addresses on ass.AddressID equals add.AddressID select new {ass, add });
            //var searchedAssets = (from ass in _context.Assets select ass);

            if (ModelState.IsValid)
            {
                if (userContext.sessionID != null)
                    search.UserID = int.Parse(userContext.sessionID);
                else search.UserID = null;
                _context.Add((Search)search);
                await _context.SaveChangesAsync();
            }
            //Enums
            searchedAssets = searchedAssets.Where(a => a.ass.TypeId == search.TypeIdIn && a.ass.Furnished == search.FurnishedIn);

            //Date
            if (search.MinEntryDate != null)
                searchedAssets = searchedAssets.Where(a => a.ass.EntryDate <= search.MinEntryDate);

            //Boolians
            if (search.IsAccessible == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsAccessible == search.IsAccessible);
            if (search.IsElevator == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsElevator == search.IsElevator);
            if (search.IsBalcony == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsBalcony == search.IsBalcony);
            if (search.IsTerrace == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsTerrace == search.IsTerrace);
            if (search.IsStorage == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsStorage == search.IsStorage);
            if (search.IsRenovated == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsRenovated == search.IsRenovated);
            if (search.IsRealtyCommission == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsRealtyCommission == search.IsRealtyCommission);
            if (search.IsAircondition == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsAircondition == search.IsAircondition);
            if (search.IsPandorDoors == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsPandorDoors == search.IsPandorDoors);
            if (search.IsMamad == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsMamad == search.IsMamad);
            if (search.IsAccessible == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsAccessible == search.IsAccessible);
            if (search.IsKosherKitchen == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsKosherKitchen == search.IsKosherKitchen);
            if (search.IsKosherBoiler == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsKosherBoiler == search.IsKosherBoiler);
            if (search.IsOnPillars == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsOnPillars == search.IsOnPillars);
            if (search.IsBars == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsBars == search.IsBars);
            if (search.IsRoomates == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsRoomates == search.IsRoomates);
            if (search.IsImmediate == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsImmediate == search.IsImmediate);
            if (search.IsNearTrainStation == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsNearTrainStation == search.IsNearTrainStation);
            if (search.IsNearLightTrainStation == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsNearLightTrainStation == search.IsNearLightTrainStation);
            if (search.IsNearBeach == true)
                searchedAssets = searchedAssets.Where(a => a.ass.IsNearBeach == search.IsNearBeach);

           // searchedAssets = searchedAssets.Where(_context.Addresses.Where(a=>a.AddressID==sear))

            //address
            if (search.City != null)
                searchedAssets = searchedAssets.Where(a => a.add.City.Contains(search.City));
            if (search.Street!= null)
                searchedAssets = searchedAssets.Where(a => a.add.Street == search.Street);
            if (search.Neighborhoods != null)
                searchedAssets = searchedAssets.Where(a => a.add.Neighborhood == search.Neighborhoods);

            //sizes
            if (search.MinPrice != null)
                searchedAssets = searchedAssets.Where(a=> a.ass.Price <= search.MinPrice);
            if (search.MaxPrice != null)
                searchedAssets = searchedAssets.Where(a => a.ass.Price >= search.MaxPrice);

            if (search.MinSize != null)
                searchedAssets = searchedAssets.Where(a => a.ass.Size <= search.MinSize);
            if (search.MaxSize != null)
                searchedAssets = searchedAssets.Where(a => a.ass.Size >= search.MaxSize);

            if (search.MinGardenSize != null)
                searchedAssets = searchedAssets.Where(a => a.ass.GardenSize <= search.MinGardenSize);
            if (search.MaxGardenSize != null)
                searchedAssets = searchedAssets.Where(a => a.ass.GardenSize >= search.MaxGardenSize);

            if (search.MinRooms != null)
                searchedAssets = searchedAssets.Where(a => a.ass.Rooms <= search.MinRooms);
            if (search.MaxRooms != null)
                searchedAssets = searchedAssets.Where(a => a.ass.Rooms >= search.MaxRooms);

            if (search.MinFloor != null)
                searchedAssets = searchedAssets.Where(a => a.ass.Floor <= search.MinFloor);
            if (search.MaxFloor != null)
                searchedAssets = searchedAssets.Where(a => a.ass.Floor >= search.MaxFloor);

            if (search.MinTotalFloor != null)
                searchedAssets = searchedAssets.Where(a => a.ass.TotalFloor <= search.MinTotalFloor);
            if (search.MaxTotalFloor != null)
                searchedAssets = searchedAssets.Where(a => a.ass.TotalFloor >= search.MaxTotalFloor);

            //date
            if(search.MinEntryDate != null)
                searchedAssets = searchedAssets.Where(a => a.ass.EntryDate <= search.MinEntryDate);

            TempData["searchedAssets"] = searchedAssets.Select(a => a.ass.AssetID).ToList<int>();
            if(searchedAssets.Count()==0)
                return RedirectToAction("Index", "Assets", new { NoSearchResults =true});

            return RedirectToAction("Index", "Assets");
        }

    }
}
