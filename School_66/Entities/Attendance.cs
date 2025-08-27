using School_66.Enums;

namespace School_66.Entities;

public class Attendance
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ScheduleId { get; set; }
    public AttendanceStatus Status { get; set; }
}