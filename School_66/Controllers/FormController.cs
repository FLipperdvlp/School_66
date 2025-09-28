using Microsoft.AspNetCore.Mvc;

namespace School_66.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}