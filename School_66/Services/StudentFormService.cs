using School_66.DataBase;
using School_66.Entities;
using School_66.Interface;
using Microsoft.EntityFrameworkCore;

namespace School_66.Service;

public class StudentFormService : IStudentFormService
{
    private readonly AppDbContext _context;

    public StudentFormService(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Student> CreateFormForStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync(); 
        return student;
    }
}
