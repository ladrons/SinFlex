using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.DTO.Internal;
using Project.WebUI.Areas.Management.VMClasses;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class MovieController : Controller
    {
        IGenreManager _genreMan;
        public MovieController(IGenreManager genreMan)
        {
            _genreMan = genreMan;
        }

        //-------------------------------------//

        public IActionResult ListMovies()
        {
            return View();
        }

        //-------------------------------------//

        
        public IActionResult AddMovie()
        {
            MovieVM mvm = new MovieVM
            {
                Genres = _genreMan.GetActives()
            };
            return View(mvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovieAsync(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid) {
                MovieVM mvm = new MovieVM
                {
                    Genres = _genreMan.GetActives()
                };
                return View(mvm);
            }
            return Ok(); //ToDo: DB'ye filmin kaydı gerçekleştirilecek.
        }

        //-------------------------------------//

        public IActionResult UpdateMovie()
        {
            return View();
        }
    }
}
