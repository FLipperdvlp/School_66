using Microsoft.EntityFrameworkCore;
using School_66.DataBase;
using School_66.Interface;
using School_66.Service;

var builder = WebApplication.CreateBuilder(args);

// сервисы
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data source=School_66.db"));

builder.Services.AddScoped<IStudentFormService, StudentFormService>();

var app = builder.Build();

// пайплайн
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
