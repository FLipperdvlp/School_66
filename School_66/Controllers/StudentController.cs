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
            if (!ModelState.IsValid)
                return View(model);

            // маппим ViewModel → Entity
            var student = new Student
            {
                Name = model.FirstName,        // Имя → Name
                Surname = model.LastName,      // Прізвище → Surname
                ClassId = Guid.NewGuid(),      // временно, если класс не связан
                Email = "test@gmail.com",      // можно привязать из формы
                PhoneNumber = "+380...",       // можно привязать из формы
            };
            
            await _studentForm.CreateFormForStudent(student);

            TempData["Message"] = "Ваш запит успішно збережено!";
            return RedirectToAction("Success");
        }
        
        [HttpGet("success")]
        public IActionResult Success()
        {
            return View();
        }
    }
}