using Microsoft.EntityFrameworkCore;
using School_66.DataBase;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data source=School_66.db"));
    
}
var app = builder.Build();
{
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    
    
    app.UseStaticFiles();
    app.Run();
}