namespace School_66.Entities;

public class Schedule
{
    public Guid Id { get; set; }
    public Guid ClassSubjectId { get; set; }
    public DateTime LessonDate { get; set; }
    public int LessonNumber { get; set; }
    public string? Room { get; set; }
}