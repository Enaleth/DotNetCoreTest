using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApp.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}