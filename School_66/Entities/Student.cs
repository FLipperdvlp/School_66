namespace School_66.Entities;

public class Student : BaseEntity
{
    public Guid ClassId { get; set; }
    public decimal AverageGrade { get; set; }
}