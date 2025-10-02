using Microsoft.EntityFrameworkCore;
using School_66.DataBase;
using School_66.Interface;
using School_66.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// сервисы
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data source=School_66.db"));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/LogIn";       // Страница входа
        options.AccessDeniedPath = "/LogIn"; // Страница при недостатке прав
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IStudentFormService, StudentFormService>();
builder.Services.AddScoped<IParentFormService, ParentFormService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// пайплайн
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
