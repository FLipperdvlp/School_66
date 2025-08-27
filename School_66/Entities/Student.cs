using School_66.Enums;

namespace School_66.Entities;

public class Student : BaseEntity
{
    public Guid ClassId { get; set; }
    public StudentStatus Status { get; set; } = StudentStatus.active;
    public decimal AverageGrade { get; set; }
}