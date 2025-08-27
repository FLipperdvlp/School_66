namespace School_66.Entities;

public class StudentLesson
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ClassSubjectId { get; set; }
    public int Grade { get; set; }
    public bool? Attendance { get; set; }
}