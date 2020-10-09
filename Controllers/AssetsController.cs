using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assetify.Data;
using Assetify.Models;

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
            var assetifyContext = _context.Assets.Include(a => a.Address);
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

            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressID");
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetID,AddressID,Price,EstimatedPrice,Furnished,TypeId,Condition,Size,GardenSize,BalconySize,Rooms,Floor,TotalFloor,NumOfParking,Description,EntryDate,CreatedAt,IsElevator,IsBalcony,IsTerrace,IsStorage,IsRenovated,IsRealtyCommission,IsAircondition,IsMamad,IsPandorDoors,IsAccessible,IsKosherKitchen,IsKosherBoiler,IsOnPillars,IsBars,IsForSell,IsCommercial,IsRoomates,IsImmediate,IsNearTrainStation,IsNearLightTrainStation,IsNearBeach,IsActive,RemovedReason")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressID", asset.AddressID);
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressID", asset.AddressID);
            return View(asset);
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
    }
}
