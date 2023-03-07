using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Project.BLL.ManagerServices.Abstracts;
using Project.DTO.Internal;
using Project.ENTITIES.Models;
using Project.WebUI.Areas.Management.VMClasses;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class SeanceController : Controller
    {
        ISeanceManager _seanceMan; ISaloonManager _saloonMan;

        public SeanceController(ISeanceManager seanceMan, ISaloonManager saloonMan)
        {
            _seanceMan = seanceMan;
            _saloonMan = saloonMan;
        }

        //-------------------------------------//

        public IActionResult ListSeances()
        {
            //SeanceVM svm = new SeanceVM
            //{
            //    Seances = _seanceMan.GetAll()
            //};
            return View();
        }

        //-------------------------------------//

        public async Task<IActionResult> ActiveSeancesOfSaloon(int id)
        {
            Saloon foundSaloon = await _saloonMan.Find(id);
            if (foundSaloon == null)
            {
                TempData["WrongSaloon"] = "Lütfen geçerli bir salon seçiniz.";
                return RedirectToAction("ListSaloons", new { id });
            }

            ICollection<Seance> activeSeances = new List<Seance>();
            foreach (Seance item in foundSaloon.Seances)
            {
                if (item.Status != ENTITIES.Enums.DataStatus.Deleted)
                {
                    activeSeances.Add(item);
                }
            }

            SeanceVM svm = new SeanceVM
            {
                Saloon = foundSaloon,
                Seances = activeSeances
            };
            return View(svm);
        }

        //-------------------------------------//

        public async Task<IActionResult> CreateSeance(int id)
        {
            Saloon findSaloon = await _saloonMan.Find(id);
            if (findSaloon.Movie != null)
            {
                SeanceVM svm = new SeanceVM
                {
                    Saloon = findSaloon
                };
                return View(svm);
            }
            return RedirectToAction("AddSaloon", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeance(SeanceDTO seanceDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["MissignDateTime"] = "Başlangıç, Bitiş tarihi ve en az bir adet saat seçmek zorunludur.";
                return View();
            }
            Saloon foundSaloon = await _saloonMan.Find(id);
            List<Seance> newSeances = _seanceMan.GenerateSeance(foundSaloon, seanceDTO.StartingDate, seanceDTO.EndingDate, seanceDTO.SeanceTimes);

            if (newSeances == null || newSeances.Count <= 0)
            {
                TempData["WrongDate"] = "Başlangıç tarihi filmin vizyon tarihinden önce olamaz.";
                return View();
            }

            _seanceMan.AddRangeAsync(newSeances);
            await _seanceMan.SaveAsync();

            return RedirectToAction("ListSeances");
        }

        //-------------------------------------//

        public IActionResult UpdateSeance()
        {
            return View();
        }

        //-------------------------------------//

        public async Task<IActionResult> DeleteSeance(int id)
        {
            Seance foundSeance = await _seanceMan.Find(id);
            if (foundSeance == null)
            {

                return RedirectToAction("ActiveSeancesOfSaloon", new { id });
            }

            _seanceMan.Delete(foundSeance);
            await _seanceMan.SaveAsync();

            TempData["ProcessCompleted"] = "İlgili seans başarılı bir şekilde kaldırılmıştır.";
            return RedirectToAction("ActiveSeancesOfSaloon", "Seance", new { foundSeance.Saloon.ID });

            //return RedirectToAction("ActiveSeancesOfSaloon", new { foundSeance.ID });
        }

        //-------------------------------------//
    }
}
