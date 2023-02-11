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
        public async Task<IActionResult> Login(AppUserDTO appUserDTO)
        {
            if (!ModelState.IsValid) return View();

            HttpContext.Session.SetObject("user", await _appUserMan.GetFoundUser(appUserDTO));
            return RedirectToAction("ListMovies", "Movie", new {area = "Management"});

            //return RedirectToAction("Dashboard");
        }
        #endregion

        //-------------------------------------//

        public IActionResult Dashboard()
        {
            AppUser sessionUser = HttpContext.Session.GetObject<AppUser>("user");
            if (sessionUser != null) ViewBag.Name = sessionUser.Firstname;

            return View();
        }

        //-------------------------------------//

        //Genre ekleme işini ya buraya ya da Dashboard içine yapabilirim.

        //-------------------------------------//
    }
}
