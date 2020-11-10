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

        // GET: Searches
        public async Task<IActionResult> Index()
        {

            return View(await _context.Searches.ToListAsync());
        }

        // GET: Searches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var search = await _context.Searches
                .FirstOrDefaultAsync(m => m.SearchID == id);
            if (search == null)
            {
                return NotFound();
            }

            return View(search);
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
            var searchedAssets = (from ass in _context.Assets select ass);

            if (ModelState.IsValid)
            {
                search.UserID = 1;// TODO int.Parse(userContext.userSessionID);
                _context.Add((Search)search);
                await _context.SaveChangesAsync();
            }
            //Enums
            searchedAssets = searchedAssets.Where(a => a.TypeId == search.TypeIdIn && a.Furnished == search.FurnishedIn);

            //Date
            if (search.MinEntryDate != null)
                searchedAssets = searchedAssets.Where(a => a.EntryDate >= search.MinEntryDate);

            //Boolians
            if (search.IsAccessible == true)
                searchedAssets = searchedAssets.Where(a => a.IsAccessible == search.IsAccessible);
            if (search.IsElevator == true)
                searchedAssets = searchedAssets.Where(a => a.IsElevator == search.IsElevator);
            if (search.IsBalcony == true)
                searchedAssets = searchedAssets.Where(a => a.IsBalcony == search.IsBalcony);
            if (search.IsTerrace == true)
                searchedAssets = searchedAssets.Where(a => a.IsTerrace == search.IsTerrace);
            if (search.IsStorage == true)
                searchedAssets = searchedAssets.Where(a => a.IsStorage == search.IsStorage);
            if (search.IsRenovated == true)
                searchedAssets = searchedAssets.Where(a => a.IsRenovated == search.IsRenovated);
            if (search.IsRealtyCommission == true)
                searchedAssets = searchedAssets.Where(a => a.IsRealtyCommission == search.IsRealtyCommission);
            if (search.IsAircondition == true)
                searchedAssets = searchedAssets.Where(a => a.IsAircondition == search.IsAircondition);
            if (search.IsPandorDoors == true)
                searchedAssets = searchedAssets.Where(a => a.IsPandorDoors == search.IsPandorDoors);
            if (search.IsMamad == true)
                searchedAssets = searchedAssets.Where(a => a.IsMamad == search.IsMamad);
            if (search.IsAccessible == true)
                searchedAssets = searchedAssets.Where(a => a.IsAccessible == search.IsAccessible);
            if (search.IsKosherKitchen == true)
                searchedAssets = searchedAssets.Where(a => a.IsKosherKitchen == search.IsKosherKitchen);
            if (search.IsKosherBoiler == true)
                searchedAssets = searchedAssets.Where(a => a.IsKosherBoiler == search.IsKosherBoiler);
            if (search.IsOnPillars == true)
                searchedAssets = searchedAssets.Where(a => a.IsOnPillars == search.IsOnPillars);
            if (search.IsBars == true)
                searchedAssets = searchedAssets.Where(a => a.IsBars == search.IsBars);
            if (search.IsRoomates == true)
                searchedAssets = searchedAssets.Where(a => a.IsRoomates == search.IsRoomates);
            if (search.IsImmediate == true)
                searchedAssets = searchedAssets.Where(a => a.IsImmediate == search.IsImmediate);
            if (search.IsNearTrainStation == true)
                searchedAssets = searchedAssets.Where(a => a.IsNearTrainStation == search.IsNearTrainStation);
            if (search.IsNearLightTrainStation == true)
                searchedAssets = searchedAssets.Where(a => a.IsNearLightTrainStation == search.IsNearLightTrainStation);
            if (search.IsNearBeach == true)
                searchedAssets = searchedAssets.Where(a => a.IsNearBeach == search.IsNearBeach);


            //address
            if (search.City != null)
                searchedAssets = searchedAssets.Where(a => a.Address.City == search.City);
            if (search.Street!= null)
                searchedAssets = searchedAssets.Where(a => a.Address.Street == search.Street);
            if (search.Neighborhoods != null)
                searchedAssets = searchedAssets.Where(a => a.Address.Neighborhood == search.Neighborhoods);

            //sizes
            if (search.MinPrice != null)
                searchedAssets = searchedAssets.Where(a=> a.Price >= search.MinPrice);
            if (search.MaxPrice != null)
                searchedAssets = searchedAssets.Where(a => a.Price >= search.MaxPrice);

            if (search.MinSize != null)
                searchedAssets = searchedAssets.Where(a => a.Size >= search.MinSize);
            if (search.MaxSize != null)
                searchedAssets = searchedAssets.Where(a => a.Size >= search.MaxSize);

            if (search.MinGardenSize != null)
                searchedAssets = searchedAssets.Where(a => a.GardenSize >= search.MinGardenSize);
            if (search.MaxGardenSize != null)
                searchedAssets = searchedAssets.Where(a => a.GardenSize >= search.MaxGardenSize);

            if (search.MinRooms != null)
                searchedAssets = searchedAssets.Where(a => a.Rooms >= search.MinRooms);
            if (search.MaxRooms != null)
                searchedAssets = searchedAssets.Where(a => a.Rooms >= search.MaxRooms);

            if (search.MinFloor != null)
                searchedAssets = searchedAssets.Where(a => a.Floor >= search.MinFloor);
            if (search.MaxFloor != null)
                searchedAssets = searchedAssets.Where(a => a.Floor >= search.MaxFloor);

            if (search.MinTotalFloor != null)
                searchedAssets = searchedAssets.Where(a => a.TotalFloor >= search.MinTotalFloor);
            if (search.MaxTotalFloor != null)
                searchedAssets = searchedAssets.Where(a => a.TotalFloor >= search.MaxTotalFloor);

            //date
            if(search.MinEntryDate != null)
                searchedAssets = searchedAssets.Where(a => a.EntryDate <= search.MinEntryDate);

            TempData["searchedAssets"] = searchedAssets.Select(a => a.AssetID).ToList<int>();

            return RedirectToAction("Index", "Assets");
        }

        // GET: Searches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var search = await _context.Searches.FindAsync(id);
            if (search == null)
            {
                return NotFound();
            }
            return View(search);
        }

        

        // GET: Searches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var search = await _context.Searches
                .FirstOrDefaultAsync(m => m.SearchID == id);
            if (search == null)
            {
                return NotFound();
            }

            return View(search);
        }

        // POST: Searches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var search = await _context.Searches.FindAsync(id);
            _context.Searches.Remove(search);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SearchExists(int id)
        {
            return _context.Searches.Any(e => e.SearchID == id);
        }
    }
}
