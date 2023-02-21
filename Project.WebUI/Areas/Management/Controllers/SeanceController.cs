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
            SeanceVM svm = new SeanceVM
            {
                Seances = _seanceMan.GetAll()
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
    }
}
