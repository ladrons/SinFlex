using Microsoft.AspNetCore.Mvc;

namespace Project.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class SeanceController : Controller
    {
        public IActionResult Test()
        {
            return View();
        }
    }
}
