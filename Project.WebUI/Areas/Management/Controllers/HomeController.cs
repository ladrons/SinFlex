using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.DTO.Internal;
using Project.ENTITIES.Models;
using Project.WebUI.Areas.Management.VMClasses;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class HomeController : Controller
    {
        IAppUserManager _appUserMan; ISaloonManager _saloonMan; IGenreManager _genreMan;

        public HomeController(IAppUserManager appUserMan, ISaloonManager saloonMan, IGenreManager genreMan)
        {
            _appUserMan = appUserMan;
            _saloonMan = saloonMan;
            _genreMan = genreMan;
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
            return RedirectToAction("ListMovies", "Movie", new { area = "Management" });

            //return RedirectToAction("Dashboard");
        }
        #endregion

        //-------------------------------------//



        //-------------------------------------//




        public IActionResult Dashboard()
        {
            AppUser sessionUser = HttpContext.Session.GetObject<AppUser>("user");
            if (sessionUser != null) ViewBag.Name = sessionUser.Firstname;

            List<Saloon> allSaloon = _saloonMan.GetAll().ToList();
            if (allSaloon == null)
            {
                
            }

            DashboardVM dvm = new DashboardVM
            {
                Saloons = allSaloon
            };

            return View(dvm);
        }

        //-------------------------------------//
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            Genre newGenre = new Genre
            {
                Name = genre.Name,
            };
            await _genreMan.AddAsync(newGenre);
            await _genreMan.SaveAsync();

            return RedirectToAction("Dashboard");
        }

        //-------------------------------------//
    }
}
