using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

[Route("Account")]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("LogIn")]
    public IActionResult LogIn() => View();

    [HttpPost("LogIn")]
    public async Task<IActionResult> LogIn(string email, string password)
    {
        var user = await _userService.GetUserByEmailAndPasswordAsync(email, password);
        if(user != null)
        {
            string role = email == "admin@gmail.com" ? "Admin" : "User";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            Console.WriteLine("User logged in: " + user.Email);
            TempData["SuccessMessage"] = "Ви успішно увійшли!";
            return RedirectToAction("Index", "Home"); // Редирект на главную
        }

        ModelState.AddModelError("", "Невірний email або пароль");//maybe change(remove) !
        TempData["ErrorMessage"] = "Ви ввели неправильну пошту або пароль!";
        return View();
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        // Разлогиниваем пользователя
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Показываем уведомление и возвращаем на главную
        TempData["SuccessMessage"] = "Ви вийшли з акаунту.";
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("Register")]
    public IActionResult Register() => View();

    [HttpPost("Register")]
    public async Task<IActionResult> Register(string fullName, string email, string password, string confirmPassword)
    {
        if(password != confirmPassword)
        {
            ModelState.AddModelError("", "Паролі не співпадають");
            return View();
        }

        if(await _userService.IsEmailTakenAsync(email))
        {
            ModelState.AddModelError("", "Користувач з таким email вже існує");
            return View();
        }

        var newUser = new User
        {
            FullName = fullName,
            Email = email,
            Password = password // Лучше хэшировать!
        };

        await _userService.CreateUserAsync(newUser);

        TempData["SuccessMessage"] = "Ви успішно зареєструвалися!";
        
        return RedirectToAction("LogIn");
    }
}