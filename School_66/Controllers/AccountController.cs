using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.CodeDom.Compiler;
using Microsoft.AspNetCore.Authentication.Google;

[Route("Account")]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    #region Email/Password Authentication

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

    #endregion

    #region Google Authentication

    [HttpGet("GoogleLogin")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = Url.Action("GoogleResponse", "Account");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("GoogleResponse")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Помилка при вході через Google.";
            return RedirectToAction("LogIn");
        }

        var email = result.Principal.FindFirstValue(ClaimTypes.Email);
        var name = result.Principal.FindFirstValue(ClaimTypes.Name);

        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            // Создаем нового пользователя
            user = new User
            {
                FullName = User.FindFirstValue(ClaimTypes.Name) ?? email!,
                Email = email,
                AuthProvider = "Google",
                Role = "User",
                Password = "" // оставляем пустым, т.к. авторизация через Google
            };
            await _userService.CreateUserAsync(user);
        }

        // Логиним пользователя
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName ?? user.Email!),
            new Claim(ClaimTypes.Role, user.Role ?? "User")
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));


        TempData["SuccessMessage"] = "Ви увійшли через Google!";
        return RedirectToAction("Index", "Home");
    }
    
    #endregion
}