using Microsoft.EntityFrameworkCore;
using School_66.Entities;

namespace School_66.DataBase;

public class AppDbContext : DbContext
{
    public required DbSet<Teacher> Teachers { get; set; }
    public required DbSet<Student> Students { get; set; }
    public required DbSet<Subject> Subjects { get; set; }
    public required DbSet<ClassSubject> ClassSubjects { get; set; }
    public required DbSet<Class> Classes { get; set; }
    public required DbSet<HomeWork> HomeWorks { get; set; }
    public required DbSet<Schedule> Schedules { get; set; }
    public required DbSet<Grades> Grades { get; set; }
    public required DbSet<StudentLesson> StudentLessons { get; set; }
    
    public AppDbContext() { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = School_66.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: soon
    }
}