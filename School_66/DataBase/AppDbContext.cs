using Microsoft.EntityFrameworkCore;
using School_66.Entities;

namespace School_66.DataBase;

public class AppDbContext : DbContext
{
    public required DbSet<StudentForm> StudentForms { get; set; }
    public required DbSet<Student> Students { get; set; }
    public required DbSet<User> Users { get; set; }
    public required DbSet<Request> Requests { get; set; }
    public required DbSet<Parent> Parents { get; set; }

    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = School_66.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: soon
    }
}