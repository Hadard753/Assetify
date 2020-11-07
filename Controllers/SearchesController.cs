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
            var allAssets = from ass in _context.Assets select ass;
            if (ModelState.IsValid)
            {
                search.UserID = 1;// int.Parse(userContext.userSessionID);
                _context.Add(search);
                await _context.SaveChangesAsync();
                // redirect after adding? return RedirectToAction(nameof(Index));
            }

            //address
            allAssets = allAssets.Where(a => a.Address.City == search.City && a.Address.Street == search.Street);

            //sizes
            allAssets = allAssets.Where(a => ((a.Price >= search.MinPrice && a.Price <= search.MaxPrice) &&
            (a.Size >= search.MinSize && a.Size <= search.MaxSize) &&
            (a.GardenSize >= search.MinGardenSize && a.GardenSize <= search.MaxGardenSize) &&
            (a.Rooms >= search.MinRooms && a.Rooms <= search.MaxRooms) &&
            (a.Floor >= search.MinFloor && a.Floor <= search.MaxFloor) &&
            (a.TotalFloor >= search.MinTotalFloor && a.Floor <= search.MaxTotalFloor)));

            allAssets.Include(a => a.Address);

            return RedirectToAction("index", "Assets", new { searchList = allAssets});
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

        // POST: Searches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SearchID,UserID,IsCommercial,City,Street,Neighborhoods,IsForSell,MinPrice,MaxPrice,MinSize,MaxSize,MinGardenSize,MaxGardenSize,MinRooms,MaxRooms,MinFloor,MaxFloor,MinTotalFloor,MaxTotalFloor,TypeIdIn,MinEntryDate,FurnishedIn,Orientations,IsElevator,IsBalcony,IsTerrace,IsStorage,IsRenovated,IsRealtyCommission,IsAircondition,IsMamad,IsPandorDoors,IsAccessible,IsKosherKitchen,IsKosherBoiler,IsOnPillars,IsBars,IsRoomates,IsImmediate,IsNearTrainStation,IsNearLightTrainStation,IsNearBeach")] Search search)
        {
            if (id != search.SearchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(search);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SearchExists(search.SearchID))
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
