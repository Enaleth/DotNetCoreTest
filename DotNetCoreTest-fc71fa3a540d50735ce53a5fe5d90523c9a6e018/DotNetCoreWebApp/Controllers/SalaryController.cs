using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApp.Controllers
{
    public class SalaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
