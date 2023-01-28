using Microsoft.AspNetCore.Mvc;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class SaloonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
