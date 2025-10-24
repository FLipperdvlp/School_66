using Microsoft.EntityFrameworkCore;
using School_66.DataBase;
using School_66.Interface;
using School_66.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data source=School_66.db"));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/LogIn";     
        options.AccessDeniedPath = "/LogIn"; 
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IStudentFormService, StudentFormService>();
builder.Services.AddScoped<IParentFormService, ParentFormService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<TelegramBotService>();

var app = builder.Build();


app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var telegramService = scope.ServiceProvider.GetRequiredService<TelegramBotService>();

    _ = Task.Run(async () =>
    {
        try
        {
            await telegramService.SendMessageAsync("✅ Hello 👋 — test message from School_66 bot!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Telegram test failed: {ex.Message}");
        }
    });
}
app.Run();
