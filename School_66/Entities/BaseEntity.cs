using System.ComponentModel.DataAnnotations;

namespace School_66.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
}