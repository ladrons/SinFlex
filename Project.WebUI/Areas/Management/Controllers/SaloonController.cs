using Microsoft.AspNetCore.Mvc;
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
        ISaloonManager _saloonMan;

        public SaloonController(ISaloonManager saloonMan)
        {
            _saloonMan = saloonMan;
        }

        public IActionResult ListSaloons()
        {
            SaloonVM svm = new SaloonVM
            {
                Saloons = _saloonMan.GetActives()
            };
            return View(svm);
        }

        //-------------------------------------//

        public IActionResult AddSaloon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSaloonAsync(SaloonDTO saloonDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Saloon findSaloon = await _saloonMan.FirstOrDefault(x => x.Name == saloonDTO.Name);
            if (findSaloon == null)
            {
                await _saloonMan.AddAsync(_saloonMan.ConvertFromDTO(saloonDTO));
                await _saloonMan.SaveAsync();

                TempData["ProcessCompleted"] = "Kayıt işlemi başarılı bir şekilde gerçekleştirildi.";
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
        public async Task<IActionResult> UpdateSaloonAsync(SaloonDTO saloonDTO)
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

            return RedirectToAction("ListSaloons"); //ToDo: silme işlemi test edilecek. (DB'de admin tanımlı değil tanımla öyle test et)
        }
    }
}
