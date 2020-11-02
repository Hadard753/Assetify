using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assetify.Data;
using Assetify.Models;
using System.Web;
using System.Diagnostics.Eventing.Reader;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Assetify.Service;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Assetify.Controllers
{
    public class UsersController : Controller
    {
        private readonly AssetifyContext _context;
       
        public UsersController(AssetifyContext context)
        {
            _context = context;
        }
        

        public ActionResult Login ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String FirstName, String Password, String message = "")

        {
            foreach (var u in _context.Users)
            {
                if (u.FirstName == FirstName && u.Password == Password)
                {
                    if (u.IsAdmin)
                        HttpContext.Session.SetString("AdminIDSession", u.UserID.ToString());

                    HttpContext.Session.SetString("UserIDSession", u.UserID.ToString());
                    
                    return RedirectToAction("Index", "home");
                }
            }
            ViewBag.Error = "Login failed, name or password is incorrect!";
            return View();
        }

        public ActionResult Logout()
        {
            var userContext = UserContextService.GetUserContext(HttpContext);
            userContext.sessionID = null;
            userContext.sessionID = null;
            userContext.isAdmin = false;
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users", new {FirstName = "", Password = "", message  = "You just logged out :)" } );
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Email,Password,FirstName,LastName,Phone,IsVerified,ProfileImgPath,LastSeenFavorite,LastSeenMessages")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult EditMyProfile()
        {
            UserContext userContext = UserContextService.GetUserContext(HttpContext);
            if (userContext.sessionID == null)
                return RedirectToAction("Login");

            return RedirectToAction("Edit", "Users", new { id = userContext.sessionID });
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            UserContext userContext = UserContextService.GetUserContext(HttpContext);
            //Check that this is an Admin or the user signed in
            if (!userContext.isAdmin && id.ToString()!= userContext.sessionID)
            {
                return RedirectToAction("EditMyProfile"); // TODO: no permission error
            }
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.isAdmin = userContext.isAdmin;
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Email,Password,FirstName,LastName,Phone,IsVerified,ProfileImgPath,LastSeenFavorite,LastSeenMessages")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if(UserContextService.GetUserContext(HttpContext).isAdmin)
                {
                    return RedirectToAction(nameof(Index));
                } else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
