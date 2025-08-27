namespace School_66.Entities;

public class HomeWork
{
    public Guid Id { get; set; }
    public Guid ClassSubjectId { get; set; }
    public string? Task { get; set; }
    public DateTime DeadLine { get; set; }
}