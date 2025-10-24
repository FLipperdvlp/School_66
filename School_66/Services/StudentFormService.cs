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

    public async Task<StudentForm> CreateFormForStudent(StudentForm form)
    {
        _context.StudentForms.Add(form);
        await _context.SaveChangesAsync();
        return form;
    }
    
    // public async Task<List<StudentForm>> GetAllFormsAsync()
    // {
    //     return await _context.StudentForms
    //         .OrderByDescending(f => f.SubmittedAt)
    //         .ToListAsync();
    // }
}
