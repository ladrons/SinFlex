using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
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
            //Eğer bu kısım da 'RedirectToAction' kullanıp AddMovie GET kısmına geri yönlendirirsem UI da validation uyarıları çıkmıyor. Bu kısmı kontrol edip daha farklı şekilde yapılabilir mi diye bakacağım.
            if (!ModelState.IsValid) 
            { 
                MovieVM mvm = new MovieVM
                {
                    Genres = _genreMan.GetActives()
                };
                return View(mvm);
            } //Eğer validation da hata olmadıysa işlemler aşağıdan devam edecek;

            if (await _movieMan.CheckSameMovie(movieDTO) == true)
            {
                Genre findGenre = await _genreMan.FirstOrDefault(x => x.Name == movieDTO.Genre);
                    //findGenre nesnesinin null gelme durumu söz konusu olmadığı için kontrol yapılmamıştır.
                
                Movie saveMovie = new Movie
                {
                    Title = movieDTO.Title,
                    Content = movieDTO.Content,
                    Genre = findGenre,
                    Duration = movieDTO.Duration,
                    ReleaseDate = movieDTO.ReleaseDate
                };
                
                await _movieMan.AddAsync(saveMovie); 
                await _movieMan.SaveAsync();

                TempData["ProcessCompleted"] = "Kayıt işlemi gerçekleştirildi."; //ToDo: Kayıt Tamamlandı mesajı düzenlenecek.
                return RedirectToAction("ListMovies");
            }
            TempData["SameMovieAlert"] = String.Format("Veritabanında {0} isimli bir film bulunmaktadır.", movieDTO.Title);
            return RedirectToAction("AddMovie");
        }

        //-------------------------------------//

        public IActionResult UpdateMovie()
        {
            return View();
        }
    }
}
