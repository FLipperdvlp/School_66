using Microsoft.EntityFrameworkCore;
using School_66.DataBase;
using School_66.Interface;
using School_66.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data source=School_66.db"));

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//AUTHENTICATION CONFIGURATION
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/LogIn";     
    options.AccessDeniedPath = "/Account/LogIn"; 
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath = "/signin-google";
});


builder.Services.AddAuthentication();

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

// Создаем область для сервисов
using (var scope = app.Services.CreateScope())
{
    var telegramService = scope.ServiceProvider.GetRequiredService<TelegramBotService>();

    // Telegram уведомление
    _ = Task.Run(async () =>
    {
        try
        {
            string message =
                "🚀 Программа *School_66* успешно запущена!\n\n" +
                "🖥️ Сервер работает локально: https://localhost:5009\n" +
                "📅 Время запуска: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\n" +
                "✅ Все службы инициализированы.";        
            await telegramService.SendMessageAsync(message);
            Console.WriteLine("✅ Telegram startup message sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Telegram test failed: {ex.Message}");
        }
    });

    // Email уведомление
    _ = Task.Run(async () =>
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("School 66", "example6@gmail.com"));
            email.To.Add(MailboxAddress.Parse("example8gmail.com"));
            email.Subject = "🚀 School_66 запущена!";
            email.Body = new TextPart("plain")
            {
                Text = "Программа School_66 только что запустилась на сервере.\nВремя: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            // Получаем пароль из конфигурации (User Secrets или переменная окружения)
            var password = builder.Configuration["EmailSettings:Password"];
            await smtp.AuthenticateAsync("example6@gmail.com", password);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            Console.WriteLine("✅ Email startup message sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email sending failed: {ex.Message}");
        }
    });
}

app.Run();