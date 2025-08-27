using School_66.Enums;

namespace School_66.Entities;

public class Teacher : BaseEntity
{
    public Positions Position { get; set; }
    public bool IsSupervisor { get; set; }
}