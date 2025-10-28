using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_66.DataBase;
using System.Security.Claims;
using System.Threading.Tasks;

namespace School_66.Controllers
{
    [Authorize]
    [Route("forms")]
    public class FormController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly AppDbContext _context;

        public FormController(AppDbContext context, IRequestService requestService)
        {
            _context = context;
            _requestService = requestService;
        }

        [HttpGet("")]//forms
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("getForms")]//forms/getForms
        public async Task<IActionResult> GetForms()
        {
            var userEmail = User.Identity?.Name;

            var forms = await _context.StudentForms
                .Where(f => f.UserEmail == userEmail)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();

            return View(forms); // теперь List<StudentForm>
        }
    }
}