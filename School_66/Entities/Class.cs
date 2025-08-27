namespace School_66.Entities;

public class Class
{
    // Class
    
    public Guid ClassId { get; set; }
    public int ClassNumber { get; set; }
    public string? ClassLetter { get; set; }
    
    // Supervisor
    
    public Guid SuperVisorId { get; set; }
    public string? SuperVisorName { get; set; }
}