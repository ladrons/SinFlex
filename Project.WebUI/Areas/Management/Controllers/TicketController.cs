using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.DTO.Internal;
using Project.ENTITIES.Models;
using Project.WebUI.Areas.Management.VMClasses;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class TicketController : Controller
    {
        ITicketManager _ticketMan;

        public TicketController(ITicketManager ticketMan)
        {
            _ticketMan = ticketMan;
        }

        //-------------------------------------//

        public IActionResult ListTickets()
        {
            TicketVM tvm = new TicketVM
            {
                Tickets = _ticketMan.GetAll()
            };
            return View(tvm);
        }

        //-------------------------------------//

        public IActionResult AddTicket()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket(TicketDTO ticketDTO)
        {
            if (!ModelState.IsValid)
                return View();

            Ticket newTicket = new Ticket
            {
                Name = ticketDTO.Name,
                Type = ticketDTO.Type,
                Price = ticketDTO.Price
            };
            await _ticketMan.AddAsync(newTicket);
            await _ticketMan.SaveAsync();

            TempData["ProcessCompleted"] = "Yeni bilet türü sisteme kaydedilmiştir.";
            return RedirectToAction("ListTickets");
        }

        //-------------------------------------//

        public async Task<IActionResult> UpdateTicket(int id)
        {
            Ticket foundTicket = await _ticketMan.Find(id);
            if (foundTicket == null)
            {
                TempData["ErrorMessage"] = "Lütfen geçerli bir bilet seçiniz.";
                return RedirectToAction("ListTickets");
            }
            TicketVM tvm = new TicketVM
            {
                Ticket = foundTicket,
            };
            return View(tvm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTicket(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                //ToDo: Burada VM göndermek yerine List sayfasına yönlendirip bir TempData ile uyarı çıkartabilirim. Kullanıcı tekrardan tıklayarak işlemini ona göre yapar.
                return RedirectToAction("ListTickets");
            }
            Ticket updateTicket = await _ticketMan.Find(ticket.ID);
            //ToDo: foundTicket için if kontrolü eklenecek mi??

            updateTicket.Name = ticket.Name;
            updateTicket.Type = ticket.Type;
            updateTicket.Price = ticket.Price;

            _ticketMan.Update(updateTicket);
            await _ticketMan.SaveAsync();

            TempData["ProcessCompleted"] = "Güncelleme işlemi başarılı bir şekilde gerçekleştirildi.";
            return RedirectToAction("ListTickets");
        }

        //-------------------------------------//

        public async Task<IActionResult> DeleteTicket(int id)
        {
            Ticket foundTicket = await _ticketMan.Find(id);

            AppUser sessionUser = HttpContext.Session.GetObject<AppUser>("user");

            //ToDo: bu bileti kimin sildiğini ve nedenini sildiğini belirtecekmiyiz?

            _ticketMan.Delete(foundTicket);
            await _ticketMan.SaveAsync();

            TempData["ProcessCompleted"] = "İlgili bilet türü silinmiştir.";
            return RedirectToAction("ListTickets");
        }

        //-------------------------------------//
    }
}
