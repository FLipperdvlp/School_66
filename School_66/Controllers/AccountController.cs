using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

[Route("")]
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
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Невірний email або пароль");
        return View();
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

        return RedirectToAction("LogIn");
    }
}
