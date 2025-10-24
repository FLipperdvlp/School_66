using School_66.Entities;

namespace School_66.Interface;

public interface IStudentFormService
{
    Task<Student> CreateFormForStudent(Student student);
}