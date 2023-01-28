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
                Movies = _movieMan.GetActives(),
            };
            //ViewBag.UserInfo = (HttpContext.Session.GetObject<AppUser>("user").Username);
            return View(mvm);
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
        public async Task<IActionResult> AddMovieAsync(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid) 
            { 
                MovieVM mvm = new MovieVM
                {
                    Genres = _genreMan.GetActives()
                };
                return View(mvm);
            } //Eğer validation da hata olmadıysa işlemler aşağıdan devam edecek;

            if (await _movieMan.CheckSameMovie(movieDTO))
            {
                Genre findGenre = await _genreMan.FirstOrDefault(x => x.Name == movieDTO.Genre);

                await _movieMan.AddAsync(_movieMan.ConvertFromDTO(movieDTO, findGenre));
                await _movieMan.SaveAsync();

                TempData["ProcessCompleted"] = "Kayıt işlemi başarılı bir şekilde gerçekleştirildi.";
                return RedirectToAction("ListMovies");
            }
            TempData["SameMovieAlert"] = String.Format("Veritabanında {0} isimli bir film bulunmaktadır.", movieDTO.Title);
            return RedirectToAction("AddMovie");
        }

        //-------------------------------------//

        public async Task<IActionResult> UpdateMovie(int id)
        {
            Movie foundMovie = await _movieMan.Find(id);

            MovieVM mvm = new MovieVM
            {
                MovieDTO = _movieMan.ConvertToDTO(foundMovie),
                Genres = _genreMan.GetActives()
            };
            return View(mvm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovieAsync(MovieDTO movieDTO)
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
            Movie updateMovie = await _movieMan.Find(Convert.ToInt32(movieDTO.ID)); //ToDo: ID gelmez ise ne olur? Test edilecek.

            updateMovie.Title = movieDTO.Title; 
            updateMovie.Content = movieDTO.Content; 
            updateMovie.Duration = movieDTO.Duration; 
            updateMovie.ReleaseDate = movieDTO.ReleaseDate;

            Genre findGenre = await _genreMan.FirstOrDefault(x => x.Name == movieDTO.Genre);

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

            AppUser sessionUser = HttpContext.Session.GetObject<AppUser>("user");

            foundMovie.ReasonForDelete = ""; //ToDo: Silme nedeni bilgisini kullanıcıdan almak için bir UI yapmak lazım.
            foundMovie.WhoDeleted = sessionUser.Username;

            _movieMan.Delete(foundMovie);
            await _movieMan.SaveAsync();

            return RedirectToAction("ListMovies");
        }
    }
}