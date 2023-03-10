using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.DTO.Internal;
using Project.ENTITIES.Models;
using Project.WebUI.Areas.Management.VMClasses;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class MovieController : Controller
    {
        IGenreManager _genreMan; IMovieManager _movieMan;

        public MovieController(IGenreManager genreMan, IMovieManager movieMan)
        {
            _genreMan = genreMan;
            _movieMan = movieMan;
        }

        //-------------------------------------//

        public IActionResult ListMovies()
        {
            MovieVM mvm = new MovieVM
            {
                Movies = _movieMan.GetActives()
            };
            //ViewBag.UserInfo = (HttpContext.Session.GetObject<AppUser>("user").Username);
            return View(mvm);
        }

        //-------------------------------------//

        public IActionResult AddMovie()
        {

            MovieVM mvm = new MovieVM
            {
                Genres = _genreMan.GetActives(),
            };
            return View(mvm);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                MovieVM mvm = new MovieVM
                {
                    Genres = _genreMan.GetActives()
                };
                return View(mvm);
            }
            Genre findGenre = await _genreMan.FirstOrDefault(x => x.Name == movieDTO.Genre);
            Movie newMovie = _movieMan.ConvertFromDTO(movieDTO, findGenre);

            if (await _movieMan.CheckSameMovie(newMovie))
            {
                await _movieMan.AddAsync(_movieMan.ConvertFromDTO(movieDTO, findGenre));
                await _movieMan.SaveAsync();

                TempData["ProcessCompleted"] = "Kayıt işlemi başarılı bir şekilde gerçekleştirildi.";
                return RedirectToAction("ListMovies");
            }
            TempData["SameMovieAlert"] = String.Format("Veritabanında '{0}' isimli bir film bulunmaktadır.", movieDTO.Title);
            return RedirectToAction("AddMovie");
        }

        //-------------------------------------//

        public async Task<IActionResult> UpdateMovie(int id)
        {
            Movie foundMovie = await _movieMan.Find(id);
            if (foundMovie == null)
            {
                TempData["ErrorMessage"] = "Lütfen geçerli bir film seçiniz.";
                return RedirectToAction("ListMovies");
            }

            MovieVM mvm = new MovieVM
            {
                MovieDTO = _movieMan.ConvertToDTO(foundMovie),
                Genres = _genreMan.GetActives()
            };
            return View(mvm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovie(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                MovieVM mvm = new MovieVM
                {
                    MovieDTO = movieDTO,
                    Genres = _genreMan.GetActives()
                };
                return View(mvm);
            }
            Movie updateMovie = await _movieMan.Find(Convert.ToInt32(movieDTO.ID));
            Genre findGenre = await _genreMan.FirstOrDefault(x => x.Name == movieDTO.Genre);

            updateMovie.Title = movieDTO.Title;
            updateMovie.Content = movieDTO.Content;
            updateMovie.Duration = movieDTO.Duration;
            updateMovie.ReleaseDate = movieDTO.ReleaseDate;
            updateMovie.Genre = findGenre;

            _movieMan.Update(updateMovie);
            await _movieMan.SaveAsync();

            TempData["ProcessCompleted"] = "Güncelleme işlemi başarılı bir şekilde gerçekleştirildi.";
            return RedirectToAction("ListMovies");
        }

        //-------------------------------------//

        public async Task<IActionResult> DeleteMovie(int id)
        {
            Movie foundMovie = await _movieMan.Find(id);
            if (_movieMan.CheckIfAssigned(foundMovie))
            {
                AppUser sessionUser = HttpContext.Session.GetObject<AppUser>("user");

                foundMovie.ReasonForDelete = ""; //ToDo: Silme nedeni bilgisini kullanıcıdan almak için bir UI yapmak lazım.
                foundMovie.WhoDeleted = "TEST";  /*sessionUser.Username;*/

                _movieMan.Delete(foundMovie);
                await _movieMan.SaveAsync();

                TempData["ProcessCompleted"] = "Seçtiğiniz film başarılı bir şekilde silinmiştir.";
                return RedirectToAction("ListMovies");
            }
            TempData["DedicatedMovie"] = "Silmek istediğiniz film şuan da gösterimdedir. Lütfen filmi önce atanmış salonlardan kaldırın.";
            return RedirectToAction("ListMovies");
        }

        //-------------------------------------//
    }
}