namespace School_66.Entities;

public class Grades
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ClassSubjectId { get; set; }
    public int GradeValue { get; set; }
    public DateTime GradeDate { get; set; }
}