using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace School_66.Controllers
{
    [Authorize]
    [Route("forms")]
    public class FormController : Controller
    {
        private readonly IRequestService _requestService;

        public FormController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("getForms")]
        public async Task<IActionResult> GetForms()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("LogIn", "Account");
            }
    
            // Получаем запросы через сервис
            var requests = await _requestService.GetUserRequestsAsync(userId);
    
            return View(requests); // передаем List<Request> или List<RequestViewModel>
        }
    }
}