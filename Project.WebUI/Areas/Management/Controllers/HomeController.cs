using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.DTO.Internal;
using Project.ENTITIES.Models;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class HomeController : Controller
    {
        IAppUserManager _appUserMan;
        public HomeController(IAppUserManager appUserMan)
        {
            _appUserMan = appUserMan;
        }

        #region Login GET/POST
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(AppUserDTO appUserDTO)
        {
            if (!ModelState.IsValid) return View();

            HttpContext.Session.SetObject("user", await _appUserMan.GetFoundUser(appUserDTO));
            return RedirectToAction("Dashboard");

            //return RedirectToAction("Index", "Home", new {area = ""});
        }
        #endregion

        //-------------------------------------//-------------------------------------

        #region Dashboard GET/POST
        public IActionResult Dashboard()
        {
            AppUser sessionUser = HttpContext.Session.GetObject<AppUser>("user");
            if (sessionUser != null) ViewBag.Name = sessionUser.Firstname;

            return View();
        }
        #endregion
    }
}
