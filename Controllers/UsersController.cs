using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assetify.Data;
using Assetify.Models;

using Microsoft.AspNetCore.Http;
using Assetify.Service;
using System.Web.Helpers;

namespace Assetify.Controllers
{
    public class UsersController : Controller
    {
        private readonly AssetifyContext _context;

        public UsersController(AssetifyContext context)
        {
            _context = context;
        }

        //message is an option if you want to add it to the Login initial view
        public ActionResult Login(String FirstName, String Password, String message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Login(String FirstName, String Password, String message = "", String returnUrl = "")
        {
            foreach (var u in _context.Users)
            {
                if (u.FirstName == FirstName && (Crypto.VerifyHashedPassword(u.Password.ToString(), Password.ToString())))
                {
                    HttpContext.Session.SetString("ProfileImg", u.ProfileImgPath);
                    if (u.IsAdmin)
                        HttpContext.Session.SetString("AdminIDSession", u.UserID.ToString());

                    HttpContext.Session.SetString("UserIDSession", u.UserID.ToString());
                    ViewBag.Login = true;
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = "Login failed, name or password is incorrect!";
            if (returnUrl != "")
                return Redirect(returnUrl);
            return View();
        }

        public ActionResult Logout()
        {
            var userContext = UserContextService.GetUserContext(HttpContext);
            userContext.sessionID = null;
            userContext.sessionID = null;
            userContext.isAdmin = false;
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users", new { FirstName = "", Password = "", message = "You just logged out :)" });
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var userContext = UserContextService.GetUserContext(HttpContext);
            if (!(userContext.isAdmin))
                return RedirectToAction("Login", "Users", new { message = "You have to be an Admin to see all users, please login with admin credentials" });
            return View(new UserIndex() { users = await _context.Users.ToListAsync(), userSearch = new UserSearch() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexSearch([FromForm] UserSearch userSearch)
        {
            var filteredUsers = _context.Users.AsQueryable();
            if (userSearch.Email != null) filteredUsers = filteredUsers.Where(x => x.Email.Contains(userSearch.Email));
            if (userSearch.FirstName != null) filteredUsers = filteredUsers.Where(x => x.FirstName.Contains(userSearch.FirstName));
            if (userSearch.LastName != null) filteredUsers = filteredUsers.Where(x => x.LastName.Contains(userSearch.LastName));
            var users = await filteredUsers.ToListAsync();

            //.ToListAsync();

            return View("Index", new UserIndex() { users = users, userSearch = userSearch });
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
        public async Task<IActionResult> Create([Bind("UserID,Email,Password,FirstName,LastName,Phone,IsVerified,ProfileImgPath,LastSeenFavorite,LastSeenMessages")] User user, IFormFile file)
        {
            user.Password = Crypto.HashPassword(user.Password);
            if (ModelState.IsValid)
            {
                user.ProfileImgPath = await FileUploader.UploadFile(file);
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
            if (!userContext.isAdmin && id.ToString() != userContext.sessionID)
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
                if (UserContextService.GetUserContext(HttpContext).isAdmin)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
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
