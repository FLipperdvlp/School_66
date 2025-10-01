using Microsoft.AspNetCore.Mvc;
using School_66.Entities;
using School_66.Interface;
using School_66.Models;

namespace School_66.Controllers
{
    [Route("parents")]
    public class ParentController : Controller
    {
        private readonly IParentFormService _parentForm;

        public ParentController(IParentFormService parentForm)
        {
            _parentForm = parentForm ?? throw new ArgumentNullException(nameof(parentForm));
        }
        [HttpGet("Get")]
        public IActionResult Index()
        {
            var model = new ParentViewModel(); // обязательно создаем объект
            return View(model);
        }
        [HttpGet("createformforparent")]
        public IActionResult CreateFormForParent()
        {
            return View(new ParentViewModel());
        }
        
        [HttpPost("createformforparent")]
        public async Task<IActionResult> CreateFormForParent(ParentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // маппим ViewModel → Entity
            var parent = new Parent
            {
                Id = model.ParentId,
                FirstName = model.ParentFirstName,
                LastName = model.ParentLastName,
                ChildFullName = model.ChildFullName,
                ChildClass = model.ChildClass,
                ContactMethod = model.ContactMethod,
                RequestText = model.RequestText,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            await _parentForm.CreateFormForParent(parent);
            
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