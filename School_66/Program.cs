using Microsoft.EntityFrameworkCore;
using School_66.DataBase;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data source = School_66.db"));
    
}
var app = builder.Build();
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.Run();
}