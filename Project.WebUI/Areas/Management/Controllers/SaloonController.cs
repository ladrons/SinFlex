using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.DTO.Internal;
using Project.ENTITIES.Models;
using Project.WebUI.Areas.Management.VMClasses;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class SaloonController : Controller
    {
        ISaloonManager _saloonMan; IMovieManager _movieMan;

        public SaloonController(ISaloonManager saloonMan, IMovieManager movieMan)
        {
            _saloonMan = saloonMan;
            _movieMan = movieMan;
        }

        /// <summary>
        /// DB'de kayıtlı bütün filmleri listeler
        /// </summary>
        /// <returns>DB'de kayıtlı olan bütün filmler</returns>
        public IActionResult ListSaloons()
        {
            SaloonVM svm = new SaloonVM
            {
                Saloons = _saloonMan.GetAll()
            };
            return View(svm);
        }

        //-------------------------------------//

        /// <summary>
        /// Seçili salonun detaylarını gösterir.
        /// </summary>
        /// <param name="id">Detaylı görüntülenecek salonun ID numarası</param>
        /// <returns></returns>
        public async Task<IActionResult> SaloonDetail(int id)
        {
            Saloon foundSaloon = await _saloonMan.Find(id);

            SaloonVM svm = new SaloonVM();
            svm.Saloon = foundSaloon;

            if (foundSaloon.Movie != null)
            {
                svm.Movie = foundSaloon.Movie;
            }
            return View(svm);
        }

        //-------------------------------------//

        public IActionResult AddSaloon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSaloon(SaloonDTO saloonDTO)
        {
            if (!ModelState.IsValid)
                return View();

            Saloon findSaloon = await _saloonMan.FirstOrDefault(x => x.Name == saloonDTO.Name);
            if (findSaloon == null)
            {
                await _saloonMan.AddAsync(_saloonMan.ConvertFromDTO(saloonDTO));
                await _saloonMan.SaveAsync();

                TempData["ProcessCompleted"] = "Salon kaydı başarılı bir şekilde gerçekleştirildi.";
                return RedirectToAction("ListSaloons");
            }
            TempData["SameSaloonAlert"] = String.Format("Veritabanında '{0}' adında farklı bir salon kayıtlıdır.", saloonDTO.Name);
            return RedirectToAction("AddSaloon");
        }

        //-------------------------------------//

        public async Task<IActionResult> UpdateSaloon(int id)
        {
            Saloon foundSaloon = await _saloonMan.Find(id);

            SaloonVM svm = new SaloonVM
            {
                SaloonDTO = _saloonMan.ConvertToDTO(foundSaloon)
            };
            return View(svm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSaloon(SaloonDTO saloonDTO)
        {
            if (!ModelState.IsValid)
            {
                Saloon findSaloon = await _saloonMan.Find(Convert.ToInt32(saloonDTO.ID));

                SaloonVM svm = new SaloonVM
                {
                    SaloonDTO = _saloonMan.ConvertToDTO(findSaloon)
                };
                return View(svm);
            }

            Saloon updateSaloon = await _saloonMan.Find(Convert.ToInt32(saloonDTO.ID));

            updateSaloon.Name = saloonDTO.Name;
            updateSaloon.Capacity = Convert.ToUInt16(saloonDTO.Capacity);

            _saloonMan.Update(updateSaloon);
            await _saloonMan.SaveAsync();

            TempData["ProcessCompleted"] = "Güncelleme işlemi başarılı bir şekilde gerçekleştirildi.";
            return RedirectToAction("ListSaloons");
        }

        //-------------------------------------//

        public async Task<IActionResult> DeleteSaloon(int id)
        {
            Saloon foundSaloon = await _saloonMan.Find(id);

            AppUser sessionUser = HttpContext.Session.GetObject<AppUser>("user");

            foundSaloon.ReasonForDelete = ""; //ToDo: Silme nedeni bilgisini kullanıcıdan almak için bir UI yapmak lazım.
            foundSaloon.WhoDeleted = sessionUser.Username;

            _saloonMan.Delete(foundSaloon);
            await _saloonMan.SaveAsync();

            TempData["ProcessCompleted"] = "Silme işlemi başarılı bir şekilde gerçekleştirildi.";
            return RedirectToAction("ListSaloons");
        }

        //-------------------------------------//

        public async Task<IActionResult> AssignMovieToSaloon(int id)
        {
            Saloon foundSaloon = await _saloonMan.Find(id); //Film atanacak olan salon.
            IQueryable activeMovies = _movieMan.GetActives(); //Aktif filmleri bulduk.

            SaloonVM svm = new SaloonVM(); //Yeni bi vm nesnesi oluşturduk.

            svm.MoviesSelectList = new List<SelectListItem>(); //List nesnemizi yarattık.
            svm.Saloon = foundSaloon; //Bulduğumuz salonu vm içerisindeki değişkene attık.

            foreach (Movie item in activeMovies) //Aktif filmlerde döndük ve yarattığımız list'e ekledik.
            {
                svm.MoviesSelectList.Add(new SelectListItem { Text = item.Title, Value = item.ID.ToString() });
            }
            return View(svm);
        }

        [HttpPost]
        public async Task<IActionResult> AssignMovieToSaloon(SaloonVM svm)
        {
            Saloon foundSaloon = await _saloonMan.Find(svm.Saloon.ID);
            if (foundSaloon.Seances.Count > 0)
            {
                TempData["ActiveSeanceFound"] = "Lütfen önce bu salon için oluşturulmuş bütün seanları kaldırın.";
                return RedirectToAction("SaloonDetail", new { foundSaloon.ID });
            }

            foundSaloon.Movie = await _movieMan.Find(svm.Movie.ID);

            _saloonMan.Update(foundSaloon);
            await _saloonMan.SaveAsync();

            TempData["ProcessCompleted"] = $"{foundSaloon.Name} İsimli salona {foundSaloon.Movie.Title} isimli film atanmıştır.";
            return RedirectToAction("SaloonDetail", new { foundSaloon.ID });
        }

        //-------------------------------------//

        public async Task<IActionResult> RemoveMovieFromSaloon(int id)
        {
            Saloon foundSaloon = await _saloonMan.Find(id);

            if (foundSaloon.Seances.Count > 0)
            {
                TempData["ActiveSeanceAvailable"] = "Kaldırmak istediğiniz filmin hala aktif olan seansları mevcuttur. Lütfen önce seansları kaldırınız.";
                return RedirectToAction("SaloonDetail", new { id });
            }

            foundSaloon.MovieID = null;
            foundSaloon.Movie = null;

            _saloonMan.Update(foundSaloon);
            await _saloonMan.SaveAsync();

            TempData["ProcessCompleted"] = $"{foundSaloon.Name} İsimli salona atanmış olan film kaldırılmıştır.";
            return RedirectToAction("SaloonDetail", new { id });
        }

        //-------------------------------------//

        public async Task<IActionResult> ActivityStatusChange(int id)
        {
            Saloon foundSaloon = await _saloonMan.Find(id);
            if (foundSaloon.Movie != null)
            {
                TempData["NotNullMovieError"] = "Kapatmak istediğiniz salonda aktif olarak gösterimde olan bir film bulunmaktadır. Lütfen önce filmi gösterimden kaldırın.";
                return RedirectToAction("SaloonDetail", new { id });
            }

            //Eğer Movie null ise aktif seansta yok demektir. O yüzden salonu açma/kapama işlemi yapılabilir.
            foundSaloon.IsOpen = (foundSaloon.IsOpen == true) ? false : true;

            _saloonMan.Update(foundSaloon);
            await _saloonMan.SaveAsync();

            TempData["ProcessCompleted"] = (foundSaloon.IsOpen == true)
                ? $"{foundSaloon.Name} Başarılı bir şekilde açılmıştır!"
                : $"{foundSaloon.Name} Başarılı bir şekilde kapatılmıştır!";

            return RedirectToAction("ListSaloons");
        }

        //-------------------------------------//







        //-------------------------------------//

        //TEST TEST TEST TEST//

        public IActionResult MovieAssignment()
        {
            #region Commands
            //Action'ın işlemi: seçili olan salona seçili olan filmi atayacak.

            /*
                Yaşanabilecek durumlar;
            1- Seçili olan salona daha önce film atanmış olabilir.
            2- Sadece aktif veya güncellenmiş salonlar atanabilmeli.
            3- Sadece aktif ve güncellenmiş filmler atanabilmeli.
            4- Hatalı atama yapılamamalı.
            5-
            */

            //İşlem yapabilmem için aktif olan filmler ve salonları görebilmeliyiz. 
            #endregion
            return Ok();
        }
        public async Task<JsonResult> SaloonListAsync()
        {
            Saloon saloon = await _saloonMan.GetFirstData();
            SaloonDTO dto = new SaloonDTO();

            dto.ID = saloon.ID.ToString();
            dto.Name = saloon.Name;
            dto.Capacity = saloon.Capacity;

            return Json(dto);
        }
    }
}
