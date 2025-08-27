using Microsoft.AspNetCore.Mvc;

namespace School_66.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}