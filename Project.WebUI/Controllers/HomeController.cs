using Microsoft.AspNetCore.Mvc;
using Project.BLL.ManagerServices.Abstracts;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WebUI.Models;
using Project.WebUI.VMClasses;
using System.Diagnostics;

namespace Project.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        Customer _customer;
        public IActionResult Index()
        {
            Customer test = new Customer
            {
                Username = "Mehmet",
                Password = "123456",
            };
            HttpContext.Session.SetObject("test", test);

            return View();
        }

        public IActionResult Privacy()
        {
            _customer = HttpContext.Session.GetObject<Customer>("test");
            TempData["username"] = _customer.Username;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}