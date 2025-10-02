
namespace School_66.Models;

public class ParentViewModel
{
    public int ParentId { get; set; }
    public string ParentFirstName { get; set; } = string.Empty;
    public string ParentLastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ChildFullName { get; set; } = string.Empty;
    public string ChildClass { get; set; } = string.Empty;
    public string ContactMethod { get; set; } = string.Empty;
    public string RequestText { get; set; } = string.Empty; // Текст запиту


    public string PhoneNumber { get; set; } = string.Empty;
    public Guid Id { get; internal set; }
}