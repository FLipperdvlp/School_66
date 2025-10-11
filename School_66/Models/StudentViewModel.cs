namespace School_66.Models;

public class StudentViewModel
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string ContactMethod { get; set; } = string.Empty;
    public string RequestText { get; set; } = string.Empty; 


    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}