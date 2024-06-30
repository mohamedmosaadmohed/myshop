using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.DataAccess.Data;
using System.Security.Claims;
using myshop.Utilities;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Get User that sign in now
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string userId = claims.Value;
            return View(_context.TbapplicationUser.Where(X => X.Id != userId).ToList());
        }
        public IActionResult lockUnlock(string? Id)
        {
            var user = _context.TbapplicationUser.FirstOrDefault(X => X.Id == Id);
            if (user == null)
                return NotFound();
            // To Close
            if(user.LockoutEnd == null | user.LockoutEnd < DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1);
            }
            // To Open
            else
            {
                user.LockoutEnd = DateTime.Now;
            }
            _context.SaveChanges();
            return  RedirectToAction("Index" , "Users", new {area = "Admin"});
        }
        public IActionResult Delete(string Id) 
        {
            var user = _context.TbapplicationUser.FirstOrDefault(X => X.Id == Id);
            if (user == null)
                return NotFound();
            _context.TbapplicationUser.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
    }
}
