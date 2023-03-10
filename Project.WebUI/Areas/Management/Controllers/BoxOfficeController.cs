using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.BLL.ManagerServices.Abstracts;
using Project.ENTITIES.Models;
using Project.WebUI.Areas.Management.Models;
using Project.WebUI.Areas.Management.VMClasses;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class BoxOfficeController : Controller
    {
        IBoxOfficeManager _boxOfficeMan; IMovieManager _movieMan; ISeanceManager _seanceMan; ISaloonManager _saloonMan;

        public BoxOfficeController(IBoxOfficeManager boxOfficeMan, IMovieManager movieMan, ISeanceManager seanceMan, ISaloonManager saloonMan)
        {
            _boxOfficeMan = boxOfficeMan;
            _movieMan = movieMan;
            _seanceMan = seanceMan;
            _saloonMan = saloonMan;
        }

        //-------------------------------------//

        public IActionResult ListBoxOffices()
        {
            BoxOfficeVM bovm = new BoxOfficeVM
            {
                BoxOffices = _boxOfficeMan.GetActives()
            };

            return View(bovm);
        }

        //-------------------------------------//

        [HttpPost]
        public async Task<IActionResult> Test(int id)
        {
            //Burada koltuk ve bilet seçim işlemleri yapılacaktır. Action'a gelen 'id' parametresi ise 'SoldTicket'ı temsil etmektedir. Bu id ile ilgili bilgiye db'den ulaşarak kendisine koltuk, bilet vb bilgileri ekleyerek güncelleyeceğiz.





            // Burada gelen veriler kullanılabilir.
            // ...
            return View();
        }

        //-------------------------------------//

        public IActionResult BringActiveMovies()
        {
            //ToDo: Bu methodun sadece bir salona atanmış olan ve status durumu 'DELETED' OLMAYAN filmleri getirmesi gerek.
            BoxOfficeVM bovm = new BoxOfficeVM
            {
                //Movies = _movieMan.Where(x => x.Saloons.Count != 0 && x.Status != ENTITIES.Enums.DataStatus.Deleted).AsQueryable()
                Movies = _movieMan.GetActives()
            };
            return View(bovm);
        }

        [HttpPost]
        public async Task<IActionResult> BringActiveMovies([FromBody] SeanceInfoDTO seanceInfoDTO)
        {
            Movie foundMovie = await _movieMan.Find(seanceInfoDTO.ID); //İlgili filmi buluyoruz.

            if (foundMovie == null)
                return NotFound(); //ToDo: Hata sayfası düzenlenecek.

            if (foundMovie.Saloons.Count > 0)
            {
                //Eğer film birden fazla salonda gösterimde ise burası çalışacak.
            }
            else
            {
                Saloon foundSaloon = foundMovie.Saloons.First(); //Filmin oynadığı salonu tespit ettik.

                //Saloon ID, Tarih ve Saat bilgileri action'a gelen parametre ile aynı olan veriyi bulmamız gerek.
                DateTime seanceDate = Convert.ToDateTime(seanceInfoDTO.Date);

                Seance foundSeance = await _seanceMan.FirstOrDefault(x => x.SaloonID == seanceInfoDTO.ID && x.Date == seanceDate && x.Time == seanceInfoDTO.Time);

                
            }

            












            SoldTicket newSoldTicket = new SoldTicket();
            return RedirectToAction("Test", newSoldTicket);
        }

        //-------------------------------------//

        [HttpGet]
        public async Task<List<SeanceInfoDTO>> BringSeanceDates(int id)
        {
            List<SeanceInfoDTO> seanceInfoDTOs = new List<SeanceInfoDTO>();

            Movie foundMovie = await _movieMan.Find(id); //Seçilen filmi ID'sinden buluyoruz.
            if (foundMovie.Saloons.Count == 1) //İlgili film tek salonda gösterimde ise bu blok çalışacak.
            {
                Saloon foundSaloon = foundMovie.Saloons.First(); //İlgili filmin gösterimde olduğu salonu buluyoruz.
                foreach (Seance item in foundSaloon.Seances)
                {
                    if (seanceInfoDTOs.IsNullOrEmpty())
                    {
                        SeanceInfoDTO test = new SeanceInfoDTO
                        {
                            ID = item.ID,

                            Date = item.Date.ToShortDateString(),
                            //Time = item.Time
                        };
                        seanceInfoDTOs.Add(test);
                    }
                    else
                    {
                        int lastIndex = seanceInfoDTOs.Count - 1;
                        if (seanceInfoDTOs[lastIndex].Date != item.Date.ToShortDateString())
                        {
                            SeanceInfoDTO test = new SeanceInfoDTO
                            {
                                ID = item.ID,
                                Date = item.Date.ToShortDateString(),
                                //Time = item.Time
                            };
                            seanceInfoDTOs.Add(test);
                        }
                        else continue;
                    }
                }
            }
            return seanceInfoDTOs;
        }
        //------------//
        [HttpGet]
        public async Task<List<string>> BringSeanceTimes(int id)
        {
            Seance foundSeance = await _seanceMan.Find(id); //Gelen id ile db'de ilgili seansı buluyoruz.

            //Db'de bulunan aynı tarihte ve aynı salonID içeren bütün seansları bu listede toplayacağız.
            //List<Seance> seances = new List<Seance>();

            List<Seance> seances = _seanceMan.Where(x => x.Date == foundSeance.Date && x.SaloonID == foundSeance.SaloonID).ToList();

            //Bulduğumuz seanslardaki saat bilgilerini bu listede toplayacağız.
            List<string> times = new List<string>();
            foreach (Seance item in seances)
            {
                times.Add(item.Time);
            }
            return times;
        }

        //-------------------------------------//
    }
}

#region test
//List<Seance> seances = new List<Seance>(); //Seanslarımız için bir list oluşturuyoruz.

//Movie foundMovie = await _movieMan.Find(id); //gelen id ile filmi buluyoruz.
//if (foundMovie.Saloons.Count == 1) //İlgili film tek salonda gösterimde ise bu blok çalışacak.
//{
//    Saloon foundSaloon = foundMovie.Saloons.First(); //İlgili filmin gösterimde olduğu salonu buluyoruz.
//    foreach (Seance item in foundSaloon.Seances) //Salona atanmış seanslarda dönüyoruz.
//    {
//        if (seances.IsNullOrEmpty()) seances.Add(item); //Eğer daha önce atanmış seans yok ise direk ekleme yapılır.
//        else
//        {
//            if (seances.Any(x => x.Date != item.Date))
//            { //List içerisinde eklenmek istenen tarih ile aynı bir tarih yoksa ekleme yapılır.
//                seances.Add(item);
//            }
//        }
//    }
//}
//return seances; 
#endregion

#region test2
//[HttpGet]
//public async Task<List<string>> BringSeanceTimes(int id)
//{
//    List<string> times = new List<string>();

//    Movie foundMovie = await _movieMan.Find(id); //Seçilen filmi ID'sinden buluyoruz.
//    if (foundMovie.Saloons.Count == 1)
//    {
//        Saloon foundSaloon = foundMovie.Saloons.First(); //İlgili filmin gösterimde olduğu salonu buluyoruz.
//        foreach (var item in foundSaloon.Seances)
//        {
//            if (times.IsNullOrEmpty())
//            {
//                times.Add(item.Time);
//            }
//            else
//            {
//                foreach (var value in times)
//                {
//                    if (value == item.Time)
//                    {
//                        continue;
//                    }
//                    else
//                    {
//                        times.Add(item.Time);
//                    }
//                }
//            }
//        }
//    }
//    return times;
//}
#endregion