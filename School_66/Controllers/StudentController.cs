using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using School_66.Entities;
using School_66.Interface;
using School_66.Models;

namespace School_66.Controllers
{
    [Route("students")]
    public class StudentController : Controller
    {
        private readonly IStudentFormService _studentForm;

        public StudentController(IStudentFormService studentForm)
        {
            _studentForm = studentForm ?? throw new ArgumentNullException(nameof(studentForm));
        }
        [HttpGet("Get")]
        public IActionResult Index()
        {
            var model = new StudentViewModel(); // обязательно создаем объект
            return View(model);
        }
        [HttpGet("createformforstudent")]
        public IActionResult CreateFormForStudent()
        {
            return View(new StudentViewModel());
        }
        
        [HttpPost("createformforstudent")]
        public async Task<IActionResult> CreateFormForStudent(StudentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userEmail = User.Identity?.Name;

            var form = new StudentForm
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ClassName = model.ClassName,
                ContactMethod = model.ContactMethod ?? "Not specified",
                RequestText = model.RequestText,
                UserEmail = userEmail ?? "Anonymous",

                // Заповнення інших полів
                Title = $"{model.LastName} {model.FirstName} — запит",
                Type = "Учнівська форма",
                Status = "Новий",
                CreatedAt = DateTime.Now
            };

            await _studentForm.CreateFormForStudent(form);

            TempData["Message"] = "Ваш запит успішно збережено!";
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet("success")]
        public IActionResult Success()
        {
            return View();
        }
    }
}